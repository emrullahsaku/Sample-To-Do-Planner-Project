using ToDo.Core.Common;

namespace ToDo.Core.Entities
{
    public class Developer : BaseEntity
    {
        public int? ProviderId { get; set; }
        public string Name { get; set; }
        public int HourlyCapacity { get; set; }
        public string ProviderSource { get; set; }
    }
}
