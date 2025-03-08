
namespace Break.Module.Core.BreakWorker.OrchestratorService
{
    public class BrakeTimeEvaluator : IBrakeTimeEvaluator
    {
        private readonly IBrakeTimeDataManager _brakeTimeDataManager;
        private readonly IBrakeTimeProcessor _brakeTimeProcessor;
        private readonly ITimeHenldeLogService _timeHandleLogService;

        public BrakeTimeEvaluator(
            IBrakeTimeDataManager brakeTimeDataManager,
            IBrakeTimeProcessor brakeTimeProcessor,
            ITimeHenldeLogService timeHandleLogService)
        {
            _brakeTimeDataManager = brakeTimeDataManager;
            _brakeTimeProcessor = brakeTimeProcessor;
            _timeHandleLogService = timeHandleLogService;
        }

        public async Task<BrakeTimeEvaluationResult> EvaluateBrakeTime(int userId, bool ipStatus)
        {
            // 🟢 ოპტიმიზაცია `Task.WhenAll()`-ით
            var fetchTimeTask = _brakeTimeDataManager.FetchServiceTimeInTimeOut(userId);
            var fetchBrakeTask = _brakeTimeDataManager.FetchExistingBrakeTime(userId);
            var getBusyTask = _brakeTimeDataManager.GetBusyStatus(userId);

            await Task.WhenAll(fetchTimeTask, fetchBrakeTask, getBusyTask);

            var existingTimeInOutResponse = await fetchTimeTask;
            var existingBrakeResponse = await fetchBrakeTask;
            bool busyStatus = await getBusyTask;

            var existingBrake = existingBrakeResponse.Data; // 🟢 ვამატებთ, რომ დავაბრუნოთ

            if (!existingTimeInOutResponse.IsSuccess)
                return new BrakeTimeEvaluationResult(
                    new ResponseResultBrakeTime { EmptyResultMessage = "No valid data for user" },
                    existingBrake
                );

            var existingTimeInOut = existingTimeInOutResponse.Data;

#pragma warning disable CS8604
            var timeDto = await _brakeTimeProcessor.PrepareTimeDto(existingBrake, existingTimeInOut);
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
    }
}



public interface IBrakeTimeEvaluator
{
    Task<BrakeTimeEvaluationResult> EvaluateBrakeTime(int userId, bool ipStatus);
}





public record BrakeTimeEvaluationResult
{
    public ResponseResultBrakeTime BrakeTimeResult { get; init; }
    public BrakeTime? ExistingBrake { get; init; }

    public BrakeTimeEvaluationResult(ResponseResultBrakeTime brakeTimeResult, BrakeTime existingBrake)
    {
        BrakeTimeResult = brakeTimeResult;
        ExistingBrake = existingBrake;
    }
}

