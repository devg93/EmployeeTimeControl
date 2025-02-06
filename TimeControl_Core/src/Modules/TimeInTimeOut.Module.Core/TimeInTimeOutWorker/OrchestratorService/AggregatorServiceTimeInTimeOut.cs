
using MediatR;
using Shared.Dto;
using Shared.Records;
using Shared.Services.ModuleCommunication.Contracts;
using Shared.Services.RunTime;
using Shared.Services.Tasks.ShedulerTuplelog;
using Shared.Services.Tasks.ShedulerTuplelog.Enum;
using TimeInTimeOut.Module.Core.DAL.Mediatr.Commands;
using TimeInTimeOut.Module.Core.Dto;
using TimeInTimeOut.Module.Core.TimeInTimeOutWorker.DAL.Mediatr.Commands;
using TimeInTimeOut.Module.Core.TimeInTimeOutWorker.DAL.Mediatr.Queries;

namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.OrchestratorService
{
    public class AggregatorServiceTimeInTimeOut : IAggregatorServiceTimeInTimeOut
    {
        private readonly IMediator _mediator;
        private readonly ISendServiceToTimeInTimeOutModule GetdServiceToTimeInTimeOutModule;
        private readonly ITimeHenldeLogService timeHenldeLogService;
        public AggregatorServiceTimeInTimeOut(IMediator mediator,
        ISendServiceToTimeInTimeOutModule sendServiceToTimeInTimeOutModule, ITimeHenldeLogService timeHenldeLogService)
        => (_mediator, this.GetdServiceToTimeInTimeOutModule, this.timeHenldeLogService) =
        (mediator, sendServiceToTimeInTimeOutModule, timeHenldeLogService);

        public async Task<bool> UpdateTimeInTimeOut(ComingAndgoingResponseDto entity, bool IpStatus)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.UserId = 1;

            //   Stopwatch stopwatch = Stopwatch.StartNew();

            var exitTimeInTimeOutTask = GetDataFromBreak(entity.UserId);
            var exitBreakTask = GetDataFromTimeInTimeOut(entity.UserId);
            await Task.WhenAll(exitTimeInTimeOutTask, exitBreakTask);
            var ResponseDto = PrepareTimeDto(exitBreakTask.Result, exitTimeInTimeOutTask.Result);

            /*SpetTest
                        var exitTimeInTimeOutTask = Task.Run(() => GetDataFromBreak(entity.UserId));
                        var exitBreakTask = Task.Run(() => GetDataFromTimeInTimeOut(entity.UserId));

                        var exitTimeInTimeOutTask = await GetDataFromBreak(entity.UserId);
                        var exitBreakTask = await GetDataFromTimeInTimeOut(entity.UserId);

                        var ResponseDto = PrepareTimeDto(exitBreakTask, exitTimeInTimeOutTask);
            */

            // stopwatch.Stop();
            // Console.WriteLine($"Task.WhenAll Execution Time: {stopwatch.ElapsedMilliseconds} ms");


            var UserInfo = await timeHenldeLogService.GetTimeResult(ResponseDto, IpStatus, true, ServiceResponseType.ComingAndgoing);
            ResponseResultTimeInTimeOut brakeTimeResult = RuntimeObjectMapper.MapObject<ResponseResultTimeInTimeOut>(UserInfo);

            switch (GetResultProces(brakeTimeResult))
            {
                case "WriteDataTimeIn":
                    await WriteDataTimeIn(new TimeInWriteCommand { Id = entity.UserId, OnlineTime = DateTime.Now });
                    return true;

                case "UpdateListDataTimeIn":
                      await WriteDataTimeInUpdate(new TimeUpdateCommand { UserId = entity.UserId, Param = 'O' });
                    return true;

                case "WriteDataTimeOut":
                    await WriteDataTimeOut(new TimeOutWriCommands { Id = entity.UserId, OflineTime = DateTime.Now });
                    return true;


                case "UpdateListDataTimeOut":
                    await WriteDataTimeOutUpdate(new TimeUpdateCommand { UserId = entity.UserId, Param = 'F' });
                    return true;


                default:
                    return false;
            }


        }





        private TimeDtoReqvest PrepareTimeDto(ResponseChecker<BrakeTimeDto> existingBrake, ComingAndgoingResponseDto existingTimeInOut)
        {
            return new TimeDtoReqvest
            {
                StartTime = existingBrake?.Data?.StartTime,
                EndTime = existingBrake?.Data?.EndTime,
                OnlineTime = existingTimeInOut.OnlineTime,
                OflineTime = existingTimeInOut.OflineTime
            };
        }
        private string GetResultProces(ResponseResultTimeInTimeOut responseResultTimeInTimeOut)
        {
            if (responseResultTimeInTimeOut.LastTimeIn)
                return "WriteDataTimeOut";
            else if (responseResultTimeInTimeOut.HasOfflineRecordForToday)
                return "UpdateListDataTimeOut";


            if (!responseResultTimeInTimeOut.HasOnlineRecordForToday)
                return "WriteDataTimeIn";
            else if (responseResultTimeInTimeOut.HasOnlineRecordForToday&&!responseResultTimeInTimeOut.HasSufficientTimePassed)
                return "UpdateListDataTimeIn";

            return "error";
        }


        /*SpedTest
                private async Task<ComingAndgoingResponseDto> GetDataFromBreak(int userId)
                {
                    Console.WriteLine($"GetDataFromBreak Start - Thread ID: {Thread.CurrentThread.ManagedThreadId}");

                    var res = await _mediator.Send(new ComingAndgoingQeuries { Id = userId });
                    Console.WriteLine($"GetDataFromBreak End - Thread ID: {Thread.CurrentThread.ManagedThreadId}");
                    return res;
                }

                private async Task<ResponseChecker<BrakeTimeDto>> GetDataFromTimeInTimeOut(int userId)
                {
                    Console.WriteLine($"GetDataFromTimeInTimeOut Start - Thread ID: {Thread.CurrentThread.ManagedThreadId}");
                    var res = await GetdServiceToTimeInTimeOutModule.GetByIdAsync(userId);
                    Console.WriteLine($"GetDataFromTimeInTimeOut End - Thread ID: {Thread.CurrentThread.ManagedThreadId}");
                    return res;
                }
        */

        private async Task<bool> WriteDataTimeOut(TimeOutWriCommands timeOutWriCommands)
        => await _mediator.Send(timeOutWriCommands);
        private async Task<bool> WriteDataTimeIn(TimeInWriteCommand timeInWriteCommand)
        => await _mediator.Send(timeInWriteCommand);
        private async Task<bool> WriteDataTimeInUpdate(TimeUpdateCommand timeInUpdateCommand)
        => await _mediator.Send(timeInUpdateCommand);
        private async Task<bool> WriteDataTimeOutUpdate(TimeUpdateCommand timeInUpdateCommand)
        => await _mediator.Send(timeInUpdateCommand);
        private async Task<ComingAndgoingResponseDto> GetDataFromBreak(int UserId)
        => await _mediator.Send(new ComingAndgoingQeuries { Id = UserId });
        private async Task<ResponseChecker<BrakeTimeDto>> GetDataFromTimeInTimeOut(int UserId)
        => await GetdServiceToTimeInTimeOutModule.GetByIdAsync(UserId);


    }

}