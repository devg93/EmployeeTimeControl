

using System.Collections.Generic;

namespace Break.Module.Core.Entity
{
    public class BrakeWorkSchedule
    {
        public int Id { get; set; }
        public ICollection<DateTimeWorkSchedule>? StartTime { get; }
        public ICollection<DateTimeWorkSchedule>? EndTime { get; }
        //**************************************************************
        public int? busyId { get; set; }
        public busyChecker? busyChecker { get; set; }
        //***************************************************************
        
    }
}