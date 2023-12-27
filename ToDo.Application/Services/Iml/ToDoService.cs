using ToDo.Application.Models;
using ToDo.DataAccess.Repositories;

namespace ToDo.Application.Services.Iml
{
    public class ToDoService : IToDoService
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly ITaskRepository _taskRepository;
        public ToDoService(IDeveloperRepository developerRepository, ITaskRepository taskRepository)
        {
            this._developerRepository = developerRepository;
            this._taskRepository = taskRepository;
        }

        public async Task<IEnumerable<WeeklyScheduleViewModel>> GetAllAsync()
        {
            var viewModelList = new List<WeeklyScheduleViewModel>();

            // Provider One
            var developersProviderOne = await GenerateWeeklyScheduleAsync("ProviderOne");
            var providerOneMinimumResult = await CalculateMinimumWeeksAsync("ProviderOne");
            var providerOneViewModel = new WeeklyScheduleViewModel
            {
                WeeklySchedule = developersProviderOne,
                MinimumWeeks = providerOneMinimumResult
            };
            viewModelList.Add(providerOneViewModel);

            // Provider Two
            var developersProviderTwo = await GenerateWeeklyScheduleAsync("ProviderTwo");
            var providerTwoMinimumResult = await CalculateMinimumWeeksAsync("ProviderTwo");
            var providerTwoViewModel = new WeeklyScheduleViewModel
            {
                WeeklySchedule = developersProviderTwo,
                MinimumWeeks = providerTwoMinimumResult
            };
            viewModelList.Add(providerTwoViewModel);

            // Provider Three
            var developersProviderThree = await GenerateWeeklyScheduleAsync("ProviderThree");
            var providerThreeMinimumResult = await CalculateMinimumWeeksAsync("ProviderThree");
            var providerThreeViewModel = new WeeklyScheduleViewModel
            {
                WeeklySchedule = developersProviderThree,
                MinimumWeeks = providerThreeMinimumResult
            };
            viewModelList.Add(providerThreeViewModel);

            return viewModelList;
        }

        public async Task<IEnumerable<DeveloperSchedule>> GenerateWeeklyScheduleAsync(string provider)
        {
            var developers = await _developerRepository.GetAllAsync(d => d.ProviderSource == provider);
            var tasks = await _taskRepository.GetAllAsync();

            var groupedDevelopers = developers.GroupBy(d => d.ProviderSource);
            var taskList = tasks.Select(task => new TaskModel
            {
                Id = task.Id,
                Name = task.Name,
                Duration = task.Duration,
                Difficulty = task.Difficulty,
                AssignedDeveloper = task.DeveloperId.ToString(), 
                ProviderSource = task.ProviderSource
            }).ToList();

            var schedule = new List<DeveloperSchedule>();

            foreach (var developerGroup in groupedDevelopers)
            {
                var developerSource = developerGroup.Key;

                foreach (var developer in developerGroup)
                {
                    var developerSchedule = new DeveloperSchedule
                    {
                        DeveloperId = developer.Id,
                        DeveloperName = developer.Name,
                        AssignedTasks = new List<TaskModel>(),
                        TotalHoursWorked = 0
                    };

                    var availableHours = 45;

                    var availableTasks = taskList
                        .OrderBy(t => t.Duration)
                        .ToList();

                    foreach (var task in availableTasks.ToList())
                    {
                        if (developerSchedule.TotalHoursWorked + task.Duration <= availableHours)
                        {
                            developerSchedule.AssignedTasks.Add(task);
                            developerSchedule.TotalHoursWorked += task.Duration;
                            taskList.Remove(task);
                        }
                    }

                    schedule.Add(developerSchedule);
                }
            }

            return schedule;
        }

        public async Task<int> CalculateMinimumWeeksAsync(string provider)
        {
            var tasks = await _taskRepository.GetAllAsync(t => t.ProviderSource == provider);

            var totalDuration = tasks.Sum(t => t.Duration);
            var minimumWeeks = (int)Math.Ceiling((double)totalDuration / 45);

            return minimumWeeks;
        }
    }
}
