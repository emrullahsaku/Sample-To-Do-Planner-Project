using ToDo.API;
using ToDo.API.Middleware;
using ToDo.Application.Models.Provider;
using ToDo.Application.Services;
using ToDo.Application.Services.Iml;
using ToDo.DataAccess;
using ToDo.DataAccess.Persistence;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.Configure<ProviderConfig>(builder.Configuration.GetSection(nameof(ProviderConfig)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDataAccess(builder.Configuration)
    .AddApplication(builder.Environment);

builder.Configuration.GetSection("Settings").Get<ProviderConfig>();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IToDoService, ToDoService>();
builder.Services.AddTransient<DataProviderFacade>();
builder.Services.AddTransient<IDataProvider, DataProvider1>();
builder.Services.AddTransient<IDataProvider, DataProvider2>();
builder.Services.AddTransient<IDataProvider, DataProvider3>();
builder.Services.AddTransient<ImportDataService>();


var app = builder.Build();

using var scope = app.Services.CreateScope();

await AutomatedMigration.MigrateAsync(scope.ServiceProvider);

app.UseCors(corsPolicyBuilder =>
    corsPolicyBuilder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
);



app.UseRouting();


app.UseMiddleware<TransactionMiddleware>();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
