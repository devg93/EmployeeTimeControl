
namespace Break.Module.Api.DTO
{
    public class BrakeTimeReqveust
    {
        public int Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
     
    }


}


public class BusyCheckerDto
{
    public int Id { get; set; }
    public bool busy { get; set; }
}

public class DateTimeWorkScheduleDto
{
    public int Id { get; set; }
    public DateTime dateTime { get; set; }
}
