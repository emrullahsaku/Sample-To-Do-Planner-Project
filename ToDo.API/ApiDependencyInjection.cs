namespace ToDo.API
{
    public static class ApiDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddServices(env);

            return services;
        }

        private static void AddServices(this IServiceCollection services, IWebHostEnvironment env)
        {
        }
    }
}
