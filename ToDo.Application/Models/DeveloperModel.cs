using Newtonsoft.Json;

namespace ToDo.Application.Models
{
    public class DeveloperModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        [JsonProperty("hourly_capacity")]
        public int HourlyCapacity { get; set; }
        public int Capacity { get; set; }
        public string ProviderSource { get; set; }
    }
}
