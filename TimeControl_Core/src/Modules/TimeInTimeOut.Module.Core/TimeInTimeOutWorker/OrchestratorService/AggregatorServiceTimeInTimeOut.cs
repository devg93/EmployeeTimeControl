

using MediatR;
using Shared.Dto;
using Shared.Services.ModuleCommunication.Contracts;
using TimeInTimeOut.Module.Core.Dto;
using TimeInTimeOut.Module.Core.TimeInTimeOutWorker.DAL.Mediatr.Queries;

namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.OrchestratorService
{
    public class AggregatorServiceTimeInTimeOut : IAggregatorServiceTimeInTimeOut
    {
        private readonly IMediator _mediator;
        private readonly ISendServiceToTimeInTimeOutModule GetdServiceToTimeInTimeOutModule;
        public AggregatorServiceTimeInTimeOut(IMediator mediator,
        ISendServiceToTimeInTimeOutModule sendServiceToTimeInTimeOutModule)
        => (_mediator, this.GetdServiceToTimeInTimeOutModule) = (mediator, sendServiceToTimeInTimeOutModule);


        public async Task<bool> UpdateTimeInTimeOut(ComingAndgoingResponseDto entity, bool Status)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.UserId = 1;

            var ExitTimeInTimeOut = await _mediator.Send(new ComingAndgoingQeuries { Id = entity.UserId });
            var ExitBreake = await GetdServiceToTimeInTimeOutModule.GetByIdAsync(entity.UserId);

            var ResponseDto = PrepareTimeDto(ExitBreake, ExitTimeInTimeOut);

            // try
            // {
            //     if (entity.UserId.HasValue)
            //     {


            //         var existingLog = await _pingLogRepository.GetByIdAsync(entity.UserId.Value);
            //         var WorkSchedules = await _workScheduleRepository.GetBreakTimeById(entity.UserId.Value);
            //         var tupleHendleLogService = TupleHendleLogService.Instance;

            //         var ReqvestTuple = new TupleReqvest
            //         {
            //             EndTime = WorkSchedules?.EndTime,
            //             StartTime = WorkSchedules?.StartTime,
            //             OnlineTime = existingLog?.OnlineTime,
            //             OflineTime = existingLog?.OflineTime
            //         };
            //         if (existingLog != null)
            //         {
            //             existingLog.OnlineTime ??= new List<DateTime>();
            //             existingLog.OflineTime ??= new List<DateTime>();

            //             if (!existingLog.OnlineTime.Any(ot => ot.Date == DateTime.Today) && Status)
            //             {
            //                 existingLog.OnlineTime.Add(DateTime.Now);
            //                 return await _pingLogRepository.Save();
            //             }

            //             ResponseResultPingLog pingLogResult = (ResponseResultPingLog)
            //             await tupleHendleLogService.TimeResult(ReqvestTuple, Status, true, ResponseType.PingLog);

            //             if (pingLogResult != null && pingLogResult.LastTimeIn)
            //             {

            //                 existingLog.OflineTime.Add(DateTime.Now);
            //                 return await _pingLogRepository.Save();

            //             }
            //         }
            //         else
            //         {
            //             if (Status)
            //             {
            //                 var pingLog = new PingLog
            //                 {
            //                     UserId = entity.UserId,
            //                     OnlineTime = entity.OnlineTime,
            //                     OflineTime = entity.OflineTime
            //                 };

            //                 await _pingLogRepository.Create(pingLog);
            //                 return true;
            //             }

            //             return false;
            //         }
            //     }
            // }
            // catch (Exception ex)
            // {
            //     _logger.LogError(ex, "Database error occurred while saving changes in addTimeInService.");
            //     throw new InvalidOperationException("An error occurred while saving changes in addTimeInService.", ex);
            // }

            return false;
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
    }





}