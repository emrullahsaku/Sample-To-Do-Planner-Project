using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ToDo.Application.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public int Difficulty { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonProperty("assigned_developer")]
        public string AssignedDeveloper { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public int DeveloperId { get; set; } 
        public string ProviderSource { get; set; }
    }
}
