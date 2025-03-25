
using TimeInTimeOut.Module.Core.TimeInTimeOutWorker.CommonServices.PersistenceServiceDb;

namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.CommonServices
{
    public class TimeValidatorService : ITimeValidator
    {
        private readonly ITimeHenldeLogService _timeHenldeLogService;
        private readonly IPersistenceServiceDb _persistenceService;
        public TimeValidatorService(ITimeHenldeLogService timeHenldeLogService, IPersistenceServiceDb persistenceServiceDb)
        => (_timeHenldeLogService, _persistenceService) = (timeHenldeLogService, persistenceServiceDb);



        async Task<ResponseResultTimeInTimeOut> ITimeValidator.TimeValidatorService(int userId, bool IpStatus)
        {
            var exitTimeInTimeOutTask = GetDataFromBreak(userId);
            var exitBreakTask = GetDataFromTimeInTimeOut(userId);
            await Task.WhenAll(exitTimeInTimeOutTask, exitBreakTask);

            var ResponseDto = await PrepareTimeDto(exitBreakTask.Result, exitTimeInTimeOutTask.Result);
            var UserInfo = await _timeHenldeLogService.GetTimeResult(ResponseDto, IpStatus, true, ServiceResponseType.ComingAndgoing);
            ResponseResultTimeInTimeOut brakeTimeResult = RuntimeObjectMapper.MapObject<ResponseResultTimeInTimeOut>(UserInfo);

            return brakeTimeResult;
        }


        private async Task<TimeDtoReqvest> PrepareTimeDto(ResponseChecker<BrakeTimeDto> existingBrake, ComingAndgoingResponseDto existingTimeInOut)
        {
            return await Task.FromResult(new TimeDtoReqvest
            {
                StartTime = existingBrake?.Data?.StartTime,
                EndTime = existingBrake?.Data?.EndTime,
                OnlineTime = existingTimeInOut.OnlineTime,
                OflineTime = existingTimeInOut.OflineTime
            });
        }
        public async Task<string> GetResultProces(ResponseResultTimeInTimeOut responseResultTimeInTimeOut)
        {
            if (responseResultTimeInTimeOut.LastTimeIn)
                return await Task.FromResult("WriteDataTimeOut");
            else if (responseResultTimeInTimeOut.HasOfflineRecordForToday)
                return await Task.FromResult("UpdateListDataTimeOut");


            if (!responseResultTimeInTimeOut.HasOnlineRecordForToday)
                return await Task.FromResult("WriteDataTimeIn");
            else if (responseResultTimeInTimeOut.HasOnlineRecordForToday && !responseResultTimeInTimeOut.HasSufficientTimePassed)
                return await Task.FromResult("UpdateListDataTimeIn");

            return await Task.FromResult("error");
        }
        private async Task<ComingAndgoingResponseDto> GetDataFromBreak(int id)
        => await _persistenceService.GetDataFromBreak(id);

        private async Task<ResponseChecker<BrakeTimeDto>> GetDataFromTimeInTimeOut(int id)
        => await _persistenceService.GetDataFromTimeInTimeOut(id);


    }
}




public interface ITimeValidator
{
    Task<ResponseResultTimeInTimeOut> TimeValidatorService(int userId, bool IpStatus);
    Task<string> GetResultProces(ResponseResultTimeInTimeOut responseResultTimeInTimeOut);
}
