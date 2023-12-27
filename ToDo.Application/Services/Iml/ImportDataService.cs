using ToDo.Core.Entities;
using ToDo.DataAccess.Repositories;

namespace ToDo.Application.Services.Iml
{
    public class ImportDataService
    {
        private readonly DataProviderFacade _dataProviderFacade;
        private readonly IDeveloperRepository _developerRepository;
        private readonly ITaskRepository _taskRepository;


        public ImportDataService(DataProviderFacade dataProviderFacade,
            IDeveloperRepository developerRepository,
            ITaskRepository taskRepository)
        {
            _dataProviderFacade = dataProviderFacade;
            _developerRepository = developerRepository;
            _taskRepository = taskRepository;

        }

        public async Task ImportDataAsync()
        {
            await GetDataFromProviderOne();
            await GetDataFromProviderTwo();
            await GetDataFromProviderThree();
        }

        private async Task GetDataFromProviderOne()
        {
            var (developers, tasks) = await _dataProviderFacade.GetDataOneAsync();

            List<Developer> developerList = new List<Developer>();
            List<TaskItem> tasksList = new List<TaskItem>();


            foreach (var developer in developers)
            {
                developerList.Add(new Developer()
                {
                    ProviderId = developer.Id ?? GetUniqueDeveloperId().Id,
                    Name = developer.Name,
                    HourlyCapacity = developer.HourlyCapacity,
                    ProviderSource = "ProviderOne"
                });
            }

            foreach (var task in tasks)
            {
                tasksList.Add(new TaskItem()
                {
                    TaskId = task.Id,
                    ProviderSource = "ProviderOne",
                    Name = task.Name,
                    DeveloperId = task.AssignedDeveloper.ToString(),
                    Difficulty = task.Difficulty,
                    Duration = task.Duration,
                });
            }
            var devResult = await _developerRepository.AddRangeAsync(developerList);
            var taskResult = await _taskRepository.AddRangeAsync(tasksList);

        }

        private async Task GetDataFromProviderTwo()
        {
            var (developers, tasks) = await _dataProviderFacade.GetDataTwoAsync();

            List<Developer> developerList = new List<Developer>();
            List<TaskItem> tasksList = new List<TaskItem>();


            foreach (var developer in developers)
            {
                developerList.Add(new Developer()
                {
                    ProviderId = developer.Id,
                    Name = developer.Name,
                    HourlyCapacity = developer.HourlyCapacity,
                    ProviderSource = "ProviderTwo"
                });
            }

            foreach (var task in tasks)
            {
                tasksList.Add(new TaskItem()
                {
                    TaskId = task.Id,
                    ProviderSource = "ProviderTwo",
                    Name = task.Name,
                    DeveloperId = task.DeveloperId.ToString(),
                    Difficulty = task.Difficulty,
                    Duration = task.Duration,
                });
            }
            _ = await _developerRepository.AddRangeAsync(developerList);
            _ = await _taskRepository.AddRangeAsync(tasksList);

        }

        private async Task GetDataFromProviderThree()
        {
            var (developers, tasks) = await _dataProviderFacade.GetDataThreeAsync();

            List<Developer> developerList = new List<Developer>();
            List<TaskItem> tasksList = new List<TaskItem>();


            foreach (var developer in developers)
            {
                developerList.Add(new Developer()
                {
                    ProviderId = developer.Id ?? GetUniqueDeveloperId().Id,
                    Name = developer.Name,
                    HourlyCapacity = developer.HourlyCapacity,
                    ProviderSource = "ProviderThree"
                });
            }

            foreach (var task in tasks)
            {
                tasksList.Add(new TaskItem()
                {
                    TaskId = task.Id,
                    ProviderSource = "ProviderThree",
                    Name = task.Name,
                    DeveloperId = task.AssignedDeveloper.ToString(),
                    Difficulty = task.Difficulty,
                    Duration = task.Duration,
                });
            }
            _ = await _developerRepository.AddRangeAsync(developerList);
            _ = await _taskRepository.AddRangeAsync(tasksList);

        }

        private async Task<int?> GetUniqueDeveloperId()
        {
            return await _developerRepository.GetMaxId() + 1;
        }

        private async Task<int?> GetUniqueTaskId()
        {
            return await _taskRepository.GetMaxId() + 1;
        }
    }
}
