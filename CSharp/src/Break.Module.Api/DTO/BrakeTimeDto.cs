using System.Collections.Generic;

namespace Break.Module.Api.DTO
{
    public class BrakeTimeDto
    {
        public int Id { get; set; }
        public ICollection<DateTimeWorkScheduleDto>? StartTime { get; set; }
        public ICollection<DateTimeWorkScheduleDto>? EndTime { get; set; }
        public int? busyId { get; set; }
        public BusyCheckerDto? busyChecker { get; set; }
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
}
