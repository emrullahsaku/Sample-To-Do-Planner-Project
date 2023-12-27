using ToDo.Application.Models;

namespace ToDo.Application.Services.Iml
{
    public class DataProviderFacade
    {
        private readonly IEnumerable<IDataProvider> _dataProviders;

        public DataProviderFacade(IEnumerable<IDataProvider> dataProviders)
        {
            _dataProviders = dataProviders;
        }

        public async Task<(List<DeveloperModel> developers, List<Models.TaskModel> tasks)> GetDataOneAsync()
        {
            var service = _dataProviders.FirstOrDefault(x => x.GetType() == typeof(DataProvider1));
            return await service.GetDataAsync();
        }

        public async Task<(List<DeveloperModel> developers, List<Models.TaskModel> tasks)> GetDataTwoAsync()
        {
            var service = _dataProviders.FirstOrDefault(x => x.GetType() == typeof(DataProvider2));
            return await service.GetDataAsync();
        }

        public async Task<(List<DeveloperModel> developers, List<Models.TaskModel> tasks)> GetDataThreeAsync()
        {
            var service = _dataProviders.FirstOrDefault(x => x.GetType() == typeof(DataProvider3));
            return await service.GetDataAsync();
        }
    }
}
