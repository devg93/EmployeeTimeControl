using System;

namespace Shared.Dto
{
    public class BrakeTimeDto
    {
        public int Id { get; set; }
        public ICollection<DateTime>? StartTime { get; set; }
        public ICollection<DateTime>? EndTime { get; set; }
        public int? busyId { get; set; }
        public busyCheckerDto? busyChecker { get; set; }

    }

    public class DateTimeWorkScheduleDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }

    public class busyCheckerDto
    {
        public int Id { get; set; }
        public bool busy { get; set; }

    }
}