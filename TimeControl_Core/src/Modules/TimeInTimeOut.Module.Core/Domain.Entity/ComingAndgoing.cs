
namespace TimeInTimeOut.Module.Core.Domain.Entity
{
    public class ComingAndgoing
    {
         public int Id { get; set; }
        public ICollection<DateTime>? OnlineTime { get; set; }
        public ICollection<DateTime>? OfflineTime { get; set; }
        public int UserId { get; set; }
    }
}