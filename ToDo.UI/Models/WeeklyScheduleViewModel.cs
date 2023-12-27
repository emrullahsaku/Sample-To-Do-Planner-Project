namespace ToDo.UI.Models
{
    public class WeeklyScheduleViewModel
    {
        public IEnumerable<DeveloperSchedule> WeeklySchedule { get; set; }
        public int MinimumWeeks { get; set; }
    }

    public class DeveloperSchedule
    {
        public int DeveloperId { get; set; }
        public string DeveloperName { get; set; }
        public List<TaskModel> AssignedTasks { get; set; }
        public int TotalHoursWorked { get; set; }
    }

    public class TaskModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public int Difficulty { get; set; }
        public string ProviderSource { get; set; }
    }
}
