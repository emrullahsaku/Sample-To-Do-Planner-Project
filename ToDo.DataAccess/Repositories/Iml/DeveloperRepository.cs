using ToDo.Core.Entities;
using ToDo.DataAccess.Persistence;

namespace ToDo.DataAccess.Repositories.Iml
{
    public class DeveloperRepository : BaseRepository<Developer>, IDeveloperRepository
    {
        public DeveloperRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
