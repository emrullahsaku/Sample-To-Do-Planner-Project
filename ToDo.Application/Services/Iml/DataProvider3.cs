using Microsoft.Extensions.Options;
using ToDo.Application.Models;
using ToDo.Application.Models.Provider;

namespace ToDo.Application.Services.Iml
{
    public class DataProvider3 : IDataProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ProviderConfig _config;

        public DataProvider3(IHttpClientFactory httpClientFactory, IOptions<ProviderConfig> config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config.Value;
        }
        public async Task<(List<Models.DeveloperModel> developers, List<Models.TaskModel> tasks)> GetDataAsync()
        {
            var url = _config.ProviderThree;

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);

            var xmlContent = await response.Content.ReadAsStringAsync();
            var data = await ConvertXmlToTypeAsync<XmlProjectDataModel>(xmlContent);

            var developers = data.Developers.Select(d => new Models.DeveloperModel
            {
                Name = d.Name,
                HourlyCapacity = d.HourlyCapacity
            }).ToList();

            var tasks = data.Tasks.TaskList.Select(t => new Models.TaskModel
            {
                Id = t.TaskId,
                Name = t.TaskName,
                Duration = t.TaskDuration,
                Difficulty = t.TaskDifficulty,
                AssignedDeveloper = t.TaskAssignedTo
            }).ToList();

            return (developers, tasks);
        }

        private async Task<XmlProjectDataModel> ConvertXmlToTypeAsync<T>(string xmlContent)
        {
            using (var stringReader = new StringReader(xmlContent))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                return (XmlProjectDataModel)serializer.Deserialize(stringReader);
            }
        }
    }
}
