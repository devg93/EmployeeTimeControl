
namespace Break.Module.Core.BreakWorker.OrchestratorService
{
    public class BrakeTimeProcessor : IBrakeTimeProcessor
    {
        private readonly IServicesFactory _servicesFactory;

        public BrakeTimeProcessor(IServicesFactory servicesFactory)
        {
            _servicesFactory = servicesFactory;
        }


        public async Task<TimeDtoReqvest> PrepareTimeDto(BrakeTime existingBrake, ComingAndGoingDto existingTimeInOut)
        {
            if (existingTimeInOut is null)
            {
                return await Task.FromResult(new TimeDtoReqvest
                {
                    StartTime = existingBrake?.BrakeStartTime ?? new List<DateTime>(),
                    EndTime = existingBrake?.BrakeEndTime ?? new List<DateTime>(),
                    OnlineTime = new List<DateTime>(),
                    OflineTime = new List<DateTime>(),
                });
            }

            if (existingBrake is null)
            {
                return await Task.FromResult(new TimeDtoReqvest
                {
                    StartTime = new List<DateTime>(),
                    EndTime = new List<DateTime>(),
                });
            }

            return await Task.FromResult(new TimeDtoReqvest
            {
                StartTime = existingBrake.BrakeStartTime,
                EndTime = existingBrake.BrakeEndTime,
            });
        }



        public async Task<bool> HandleValidWorkSchedule(ResponseResultBrakeTime resultTime, BrakeTime existingBrake, int id, bool status)
        {
            if (resultTime.workSchedulPingLog)
            {
                await _servicesFactory.GetBreakRepositoryCommand().UbdateBreakAsync(id, 2);
                return await UpdateBusyStatus(id, false);
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
                await _servicesFactory.GetBreakRepositoryCommand().CreateBreakAsync(newBrakeTime);
                return await CreateBusyStatus(entity.UserId, true);
            }

            await _servicesFactory.GetBreakRepositoryCommand().UbdateBreakAsync(entity.UserId, 1);
            return await UpdateBusyStatus(entity.UserId, true);
        }

        private async Task<bool> UpdateBusyStatus(int id, bool status)
        {
            await _servicesFactory.GetBusyRepositoryCommand().UpdateBusy(id, status);
            return await _servicesFactory.GetBusyRepositoryCommand().Save();
        }

        private async Task<bool> CreateBusyStatus(int userId, bool status)
        {
            return await _servicesFactory.GetBusyRepositoryCommand().CreateBusy(userId, status);
        }
    }

    public interface IBrakeTimeProcessor
    {
        Task<TimeDtoReqvest> PrepareTimeDto(BrakeTime existingBrake, ComingAndGoingDto existingTimeInOut);
        Task<bool> HandleValidWorkSchedule(ResponseResultBrakeTime resultTime, BrakeTime existingBrake, int id, bool status);
        Task<bool> HandleOnlineTimeValid(ResponseResultBrakeTime resultTime, BrakeTimeDtoReqvest entity, bool status);
    }

}