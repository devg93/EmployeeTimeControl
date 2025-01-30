
namespace TimeInTimeOut.Module.Core.Dto
{
    public class ComingAndgoingResponseDto
    {
        public int Id { get; set; }
        public ICollection<DateTime>? OnlineTime { get; set; }
        public ICollection<DateTime>? OflineTime { get; set; }
        public int UserId { get; set; }

    }
}