using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ToDo.Application.Models;
using ToDo.Application.Models.Provider;

namespace ToDo.Application.Services.Iml
{
    public class DataProvider2 : IDataProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ProviderConfig _config;
        public DataProvider2(IHttpClientFactory httpClientFactory, IOptions<ProviderConfig> config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config.Value;
        }

        public async Task<(List<DeveloperModel> developers, List<Models.TaskModel> tasks)> GetDataAsync()
        {
            var url = _config.ProviderTwo;

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var jsonContent = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<ProjectDataModel>(jsonContent);

            var developers = data.Developers.Select(d => new Models.DeveloperModel
            {
                Id = d.Id,
                Name = d.Name,
                HourlyCapacity = d.Capacity
            }).ToList();

            var tasks = data.Tasks.Select(t => new Models.TaskModel
            {
                Id = t.Id,
                Name = t.Name,
                Duration = t.Duration,
                Difficulty = t.Difficulty,
                DeveloperId = t.DeveloperId
            }).ToList();

            return (developers, tasks);
        }

    }
}
