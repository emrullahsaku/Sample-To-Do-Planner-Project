namespace ToDo.Application.Models
{
    public class WeeklyScheduleViewModel
    {
        public IEnumerable<DeveloperSchedule> WeeklySchedule { get; set; }
        public int MinimumWeeks { get; set; }
    }
}
