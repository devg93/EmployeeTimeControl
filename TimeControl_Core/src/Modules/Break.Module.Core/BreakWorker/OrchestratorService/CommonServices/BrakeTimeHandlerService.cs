
namespace Break.Module.Core.BreakWorker.CommonServices.OrchestratorService
{
    public class BrakeTimeHandler : IBrakeTimeHandler
    {
        private readonly IPersistenceService _services;

        public BrakeTimeHandler(IPersistenceService services)
        => _services = services;




        public async Task<bool> HandleValidWorkSchedule(ResponseResultBrakeTime resultTime, BrakeTime existingBrake, int id, bool status)
        {
            if (resultTime.workSchedulPingLog)
            {
                await _services.UbdateBreakAsync(id, 2);
                return await _services.UpdateBusyStatus(id, false);
            }
            return false;
        }


        public async Task<bool> HandleOnlineTimeValid(ResponseResultBrakeTime resultTime, BrakeTimeDtoReqvest entity, bool status)
        {
            var newBrakeTime = new BrakeTime
            {
                UserId = entity.UserId,
                BrakeStartTime = entity.StartTime,
                BrakeEndTime = entity.EndTime,
            };

            if (resultTime.StartTimeBreak)
            {
                await _services.CreateBreakAsync(newBrakeTime);
                return await _services.CreateBusyStatus(entity.UserId, true);
            }

            await _services.UbdateBreakAsync(entity.UserId, 1);
            return await _services.UpdateBusyStatus(entity.UserId, true);
        }


    }

    public interface IBrakeTimeHandler
    {

        Task<bool> HandleValidWorkSchedule(ResponseResultBrakeTime resultTime, BrakeTime existingBrake, int id, bool status);
        Task<bool> HandleOnlineTimeValid(ResponseResultBrakeTime resultTime, BrakeTimeDtoReqvest entity, bool status);
    }

}