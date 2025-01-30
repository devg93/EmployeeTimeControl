
namespace TimeInTimeOut.Module.Core.Domain.Entity
{
    public class DateTimeTimeInTimeOut
    {
        public int Id { get; set; }
        public DateTime ?TimeIn { get; set; }
        public DateTime ?TimeOut { get; set; }
        public int ComingAndgoingId { get; set; }
        public ComingAndgoing? OnlineComingAndgoing { get; set; }
        public ComingAndgoing? OfflineComingAndgoing { get; set; }
    }
}