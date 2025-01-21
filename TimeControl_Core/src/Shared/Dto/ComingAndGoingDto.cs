
namespace Shared.Dto
{
  public class ComingAndGoingDto
    {
        public int Id { get; set; }
        public ICollection<DateTimeDto>? OnlineTime { get; set; }
        public ICollection<DateTimeDto>? OflineTime { get; set; }
    }

    public class DateTimeDto
    {
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
    }
}