
namespace Break.Module.Core.BreakWorker.CommonServices.OrchestratorService
{
    public class TimeValidator : ITimeValidator
    {
        private readonly IPersistenceService _brakeTimeDataManager;

        private readonly ITimeHenldeLogService _timeHandleLogService;

        public TimeValidator(
            IPersistenceService brakeTimeDataManager,

            ITimeHenldeLogService timeHandleLogService)
        {
            _brakeTimeDataManager = brakeTimeDataManager;

            _timeHandleLogService = timeHandleLogService;
        }

        public async Task<BrakeTimeEvaluationResult> TimeValidatorService(int userId, bool ipStatus)
        {

            var fetchTimeTask = _brakeTimeDataManager.FetchServiceTimeInTimeOut(userId);
            var fetchBrakeTask = _brakeTimeDataManager.FetchExistingBrakeTime(userId);
            var getBusyTask = _brakeTimeDataManager.GetBusyStatus(userId);

            await Task.WhenAll(fetchTimeTask, fetchBrakeTask, getBusyTask);

            var existingTimeInOutResponse = await fetchTimeTask;
            var existingBrakeResponse = await fetchBrakeTask;
            bool busyStatus = await getBusyTask;

            var existingBrake = existingBrakeResponse.Data;

            if (!existingTimeInOutResponse.IsSuccess) return new BrakeTimeEvaluationResult(
                 new ResponseResultBrakeTime { EmptyResultMessage = "No valid data for user" }, existingBrake ?? new BrakeTime()
                );

            var existingTimeInOut = existingTimeInOutResponse.Data;

#pragma warning disable CS8604
            var timeDto = await PrepareTimeDto(existingBrake, existingTimeInOut);
#pragma warning restore CS8604

            var userInfo = await _timeHandleLogService.GetTimeResult(timeDto, ipStatus, busyStatus, ServiceResponseType.BrakeTime);

            if (userInfo == null)
                return new BrakeTimeEvaluationResult(
                    new ResponseResultBrakeTime { EmptyResultMessage = "User info is null" },
                    existingBrake
                );

            var brakeTimeResult = RuntimeObjectMapper.MapObject<ResponseResultBrakeTime>(userInfo);

            return new BrakeTimeEvaluationResult(brakeTimeResult, existingBrake);
        }



        private async Task<TimeDtoReqvest> PrepareTimeDto(BrakeTime existingBrake, ComingAndGoingDto existingTimeInOut)
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
    }




}



public interface ITimeValidator
{
    Task<BrakeTimeEvaluationResult> TimeValidatorService(int userId, bool ipStatus);
}







