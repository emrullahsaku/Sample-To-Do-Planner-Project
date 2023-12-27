using ToDo.Application.Models;

namespace ToDo.Application.Services
{
    public interface IToDoService
    {
        Task<IEnumerable<WeeklyScheduleViewModel>> GetAllAsync();
        Task<IEnumerable<DeveloperSchedule>> GenerateWeeklyScheduleAsync(string provider);
        Task<int> CalculateMinimumWeeksAsync(string provider);
    }
}
