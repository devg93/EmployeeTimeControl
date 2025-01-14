using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeInTimeOut.Module.Core.TimeInTimeOutWorker;

namespace TimeInTimeOut.Module.Core.Exstension
{
    public class NewClass
    {
        int Id { get; set; }
        ICollection<DateTimeWorkSchedule>? StartTime { get; set; }
        ICollection<DateTimeWorkSchedule>? EndTime { get; set; }
        //**************************************************************
        int? busyId { get; set; }
        busyChecker? busyChecker { get; set; }
        //***************************************************************

    }
}