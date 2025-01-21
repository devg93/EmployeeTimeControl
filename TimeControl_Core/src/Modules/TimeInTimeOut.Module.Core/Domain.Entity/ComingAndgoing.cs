
namespace TimeInTimeOut.Module.Core.Domain.Entity
{
    public class ComingAndgoing
    {
        public int Id { get; set; }
        public ICollection<DateTimeTimeInTimeOut>? OnlineTime { get; set; }
        public ICollection<DateTimeTimeInTimeOut>? OflineTime { get; set; }


    }
}