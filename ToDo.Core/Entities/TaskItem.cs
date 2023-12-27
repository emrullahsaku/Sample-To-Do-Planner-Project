using ToDo.Core.Common;

namespace ToDo.Core.Entities
{
    public class TaskItem : BaseEntity
    {
        public int? TaskId { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public int Difficulty { get; set; }
        public string DeveloperId { get; set; } 
        public string ProviderSource { get; set; }
    }
}
