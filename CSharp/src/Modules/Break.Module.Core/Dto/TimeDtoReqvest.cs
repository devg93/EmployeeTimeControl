using System;
using System.Collections.Generic;


namespace Modules. Break.Module.Core.Dto;

    public class TimeDtoReqvest
    {
        public ICollection<DateTime>? StartTime { get; set; }
        public ICollection<DateTime>? EndTime { get; set; }
        public ICollection<DateTime>? OnlineTime { get; set; }
        public ICollection<DateTime>? OflineTime { get; set; }
    }
