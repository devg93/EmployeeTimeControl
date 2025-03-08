

using System;
using System.Collections.Generic;


namespace Modules. Break.Module.Core.Entity;

    public class BrakeTime
    {
        public int Id { get; set; }
        public ICollection<DateTime>? BrakeStartTime { get;set; }
        public ICollection<DateTime>? BrakeEndTime { get; set;}
        //**************************************************************
        public int UserId { get; set; }
        public int? busyId { get; set; }
        public busyChecker? busyChecker { get; set; }
        //***************************************************************
        
    }
