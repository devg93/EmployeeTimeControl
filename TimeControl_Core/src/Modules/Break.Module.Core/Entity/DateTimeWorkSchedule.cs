
namespace Modules.Break.Module.Core.Entity;

public class DateTimeWorkSchedule
{


    public int Id { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    
    public int BrakeTimeMapId { get; set; }
    public BrakeTime? BrakeTimeMap { get; set; }
   
}



