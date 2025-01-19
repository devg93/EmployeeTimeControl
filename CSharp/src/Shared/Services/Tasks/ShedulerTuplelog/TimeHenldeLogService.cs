
using Shared.Dto;
using Shared.Services.Tasks.ShedulerTuplelog.Enum;

namespace Shared.Services.Tasks.ShedulerTuplelog;

    public sealed class TimeHenldeLogService : ITimeHenldeLogService
    {


        public Task<object> GetTimeResult(TimeDtoReqvest entity, bool status, bool busy, ServiceResponseType responseType)
        {
           if (entity == null) throw new ArgumentNullException(nameof(entity));

            switch (responseType)
            {
                case ResponseType.BrakeTime:
                    return await Task.FromResult(new ResponseResultBrakeTime
                    {

                        StartTimeValidWorkSchedule = entity != null && entity.StartTime != null &&
                        entity.StartTime.Any() && status && busy == true,

                        EndTimeLastMinute = entity?.EndTime != null && entity.EndTime.Any() && entity.EndTime.Last().Minute == DateTime.Now.Minute,
                        StartTimeTimeLastMinute = entity?.StartTime != null && entity.StartTime.Any() && entity.StartTime.Last().Minute == DateTime.Now.Minute,
                        OnlineTimeDateDay = (entity?.OnlineTime?.Any(day => day.Day == DateTime.Now.Day) ?? false) && !status,
                        //  OnlineTimeDateDay = (entity?.OnlineTime?.Any() ?? false) && !status,??????
                        StartTimeBreak = entity?.StartTime?.Any(day => day.Day == DateTime.Now.Day) == false,
                        OfflineTimeDateDay = entity?.OflineTime?.Any(day => day.Day == DateTime.Now.Day) ?? false,
                        workSchedulPingLog = !status && entity?.StartTime?.Any()==true &&
                        entity?.OflineTime?.Any(day => day.Day == DateTime.Now.Day) == false && !busy
                    });
                case ResponseType.ComingAndgoing:
                  

                default:
                    throw new ArgumentException("Invalid response type");
            }
        }
    }
        
