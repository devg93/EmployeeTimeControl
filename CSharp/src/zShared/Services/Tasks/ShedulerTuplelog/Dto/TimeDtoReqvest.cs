
namespace zShared.Services.Tasks.ShedulerTuplelog.Dto
{
    public class TimeDtoReqvest
    {
        public ICollection<DateTime>? StartTime { get; set; }
        public ICollection<DateTime>? EndTime { get; set; }
        public ICollection<DateTime>? OnlineTime { get; set; }
        public ICollection<DateTime>? OflineTime { get; set; }
    }
}