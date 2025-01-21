using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker
{
    public class NewClasscsac
    {
        int Id { get; set; }
        ICollection<DateTimeWorkSchedule>? StartTime { get; set; }
        ICollection<DateTimeWorkSchedule>? EndTime { get; set; }
        //**************************************************************
        int? busyId { get; set; }
        busyChecker? busyChecker { get; set; }
        //***************************************************************
        

    }

    internal class busyChecker
    {
    }

    internal class DateTimeWorkSchedule
    {
    }
}