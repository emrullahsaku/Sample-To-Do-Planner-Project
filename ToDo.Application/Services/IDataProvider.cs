using ToDo.Application.Models;

namespace ToDo.Application.Services
{
    public interface IDataProvider
    {
        Task<(List<DeveloperModel> developers, List<Models.TaskModel> tasks)> GetDataAsync();
    }
}
