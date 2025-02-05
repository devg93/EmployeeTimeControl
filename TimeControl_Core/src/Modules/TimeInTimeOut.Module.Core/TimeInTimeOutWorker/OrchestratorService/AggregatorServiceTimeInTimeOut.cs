
using MediatR;
using Shared.Dto;
using Shared.Records;
using Shared.Services.ModuleCommunication;
using Shared.Services.ModuleCommunication.Contracts;
using Shared.Services.Tasks.ShedulerTuplelog;
using Shared.Services.Tasks.ShedulerTuplelog.Enum;
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


            var exitTimeInTimeOutTask = GetDataFromBreak(entity.UserId);
            var exitBreakTask = GetDataFromTimeInTimeOut(entity.UserId);

            var ExitTimeInTimeOut = await exitTimeInTimeOutTask;
            var ExitBreak = await exitBreakTask;


            var ResponseDto = PrepareTimeDto(ExitBreak, ExitTimeInTimeOut);

            var UserInfo = await timeHenldeLogService.GetTimeResult(ResponseDto, IpStatus, true, ServiceResponseType.ComingAndgoing);
            ResponseResultTimeInTimeOut brakeTimeResult = RuntimeObjectMapper.MapObject<ResponseResultTimeInTimeOut>(UserInfo);

            if (GetResultProces(brakeTimeResult))
            {
                return await WriteDataTimeIn(new TimeInWriteCommand { Id = entity.UserId, OnlineTime = DateTime.Now });
            }
            else
            {
                return await WriteDataTimeOut(new TimeOutWriCommands { Id = entity.UserId, OflineTime = DateTime.Now });
            }


        }





        private TimeDtoReqvest PrepareTimeDto(ResponseChecker<BrakeTimeDto> existingBrake, ComingAndgoingResponseDto existingTimeInOut)
        {
            return new TimeDtoReqvest
            {
                StartTime = existingBrake?.Data?.StartTime,
                EndTime = existingBrake?.Data?.EndTime,
                // OnlineTime = existingTimeInOut.OnlineTime?.Select(o => o.TimeIn).ToList(),
                // OflineTime = existingTimeInOut.OflineTime?.Select(o => o.TimeOut).ToList()
            };
        }

        private bool GetResultProces(ResponseResultTimeInTimeOut responseResultTimeInTimeOut)
        {
            return true;
        }

        private async Task<bool> WriteDataTimeOut(TimeOutWriCommands timeOutWriCommands)
        => await _mediator.Send(timeOutWriCommands);



        private async Task<bool> WriteDataTimeIn(TimeInWriteCommand timeInWriteCommand)
        => await _mediator.Send(timeInWriteCommand);

        private async Task<ComingAndgoingResponseDto> GetDataFromBreak(int UserId)
        => await _mediator.Send(new ComingAndgoingQeuries { Id = UserId });



        private async Task<ResponseChecker<BrakeTimeDto>> GetDataFromTimeInTimeOut(int UserId)
        => await GetdServiceToTimeInTimeOutModule.GetByIdAsync(UserId);


    }

}