
namespace Shared.Dto
{
  public class ComingAndGoingDto
    {
        public int Id { get; set; }
        public ICollection<DateTime>? OnlineTime { get; set; }
        public ICollection<DateTime>? OflineTime { get; set; }
    }

   
}