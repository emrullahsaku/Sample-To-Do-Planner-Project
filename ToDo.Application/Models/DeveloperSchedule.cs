namespace ToDo.Application.Models
{
    public class DeveloperSchedule
    {
        public int DeveloperId { get; set; }
        public string DeveloperName { get; set; }
        public List<TaskModel> AssignedTasks { get; set; }
        public int TotalHoursWorked { get; set; }
    }
}
