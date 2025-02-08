
namespace Modules. Break.Module.Core.Mediator;

    public class BreakTimeUpdateMediator:IBreakTimeUpdateMediator
    {
        private readonly IAggregatorServiceBrakeTime brakeTimeService;
        public BreakTimeUpdateMediator(IAggregatorServiceBrakeTime brakeTimeService)
        => this.brakeTimeService = brakeTimeService;

        public async Task<bool> UpdateBreakTimeAsync(int userId, bool pingResponseStatus)
        {
             var workSchedule = new BrakeTimeDtoReqvest
            {
                UserId = userId,
                StartTime = pingResponseStatus ? new List<DateTime>() : new List<DateTime> { DateTime.Now },
                EndTime = pingResponseStatus ? new List<DateTime> { DateTime.Now } : new List<DateTime>()
            };

             await brakeTimeService.AddOrUpdateBrakeTime(workSchedule, pingResponseStatus);
            return true;
        }
    }
