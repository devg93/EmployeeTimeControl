
namespace Modules.Break.Module.Core.Dto;

    public class BrakeTimeDtoReqvest
    {
        public int Id {  get; set; }
        public List<DateTime>? StartTime {  get; set; }
        public List<DateTime>? EndTime {  get; set; }
        public int UserId {  get; set; }

    }
