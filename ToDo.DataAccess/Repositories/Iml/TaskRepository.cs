using ToDo.Core.Entities;
using ToDo.DataAccess.Persistence;

namespace ToDo.DataAccess.Repositories.Iml
{
    public class TaskRepository : BaseRepository<TaskItem>, ITaskRepository
    {
        public TaskRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
