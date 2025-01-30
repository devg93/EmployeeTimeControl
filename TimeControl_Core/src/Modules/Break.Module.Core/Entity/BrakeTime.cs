

using System.Collections.Generic;


namespace Modules. Break.Module.Core.Entity;

    public class BrakeTime
    {
        public int Id { get; set; }
        public ICollection<DateTimeWorkSchedule>? BrakeStartTime { get;set; }
        public ICollection<DateTimeWorkSchedule>? BrakeEndTime { get; set;}
        //**************************************************************
        public int? busyId { get; set; }
        public busyChecker? busyChecker { get; set; }
        //***************************************************************
        
    }
