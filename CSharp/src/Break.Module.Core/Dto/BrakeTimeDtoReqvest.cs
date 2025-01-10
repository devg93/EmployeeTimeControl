using System;
using System.Collections.Generic;

namespace Break.Module.Core.Dto
{
    public class BrakeTimeDtoReqvest
    {
        public int Id {  get; set; }
        public List<DateTime>? StartTime { private get; set; }
        public List<DateTime>? EndTime { private get; set; }
        public int UserId { private get; set; }

    }
}