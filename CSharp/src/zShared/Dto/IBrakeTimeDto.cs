using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zShared.Dto
{
    public interface IBrakeTimeDto
    {
        public int Id { get; set; }
        public ICollection<DateTimeWorkSchedule>? StartTime { get;set; }
        public ICollection<DateTimeWorkSchedule>? EndTime { get;set; }
        //**************************************************************
        public int? busyId { get; set; }
        public busyChecker? busyChecker { get; set; }
        //***************************************************************
    }

    public class DateTimeWorkSchedule
    {
        public int Id { get; set; }
        // [Column(TypeName = "datetime")]
        public DateTime dateTime { get; set; }

    }

    public class busyChecker
    {
        public int Id { get; set; }
        public bool busy { get; set; }

    }




}