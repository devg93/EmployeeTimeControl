

namespace TimeInTimeOut.Module.Core.Dto
{
    public class ComingAndgoingDto
    {
        public int Id { get; set; }
        public List<DateTime>? OnlineTime { get; set; }
        public List<DateTime>? OflineTime { get; set; }
        public int UserId { get; set; }

    }
}