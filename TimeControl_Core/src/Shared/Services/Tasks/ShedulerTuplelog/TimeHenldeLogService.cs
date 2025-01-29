
using Shared.Dto;
using Shared.Records;
using Shared.Services.Tasks.ShedulerTuplelog.Enum;
using System;
using System.Runtime.InteropServices;

namespace Shared.Services.Tasks.ShedulerTuplelog;

public sealed class TimeHenldeLogService : ITimeHenldeLogService
{

    //***this retrunet alocted memory to the unmanaged code

    /*
        public async Task<nint> GetTimeResult(TimeDtoReqvest entity, bool status, bool busy, ServiceResponseType responseType)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            object res;
            switch (responseType)
            {
                case ServiceResponseType.BrakeTime:
                    res = await FetchExistingBrakeTime(entity, status, busy);
                    break;

                case ServiceResponseType.ComingAndgoing:
                    res = await FetchExistingComingAndgoing(entity, status, busy);
                    break;

                default:
                    throw new InvalidOperationException("Invalid ServiceResponseType");
            }


            Console.WriteLine($"Allocated type: {res.GetType().FullName}");
            return GCHandle.ToIntPtr(GCHandle.Alloc(res));
        }

    */

    public async Task<object> GetTimeResult(TimeDtoReqvest entity, bool status, bool busy, ServiceResponseType responseType)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        switch (responseType)
        {
            case ServiceResponseType.BrakeTime:

                return await FetchExistingBrakeTime(entity, status, busy); ;

            case ServiceResponseType.ComingAndgoing:

                return await FetchExistingComingAndgoing(entity, status, busy);

            default:
                return ("Invalid ServiceResponseType", nameof(responseType));
        }
    }

    //******************************************Async Methods ********************************************************************

    /// <summary>
    /// Evaluates and creates a ResponseResultBrakeTime object based on the provided TimeDtoReqvest data.
    /// </summary>
    /// <param name="obj">The object containing time-related data. Expected to be of type TimeDtoReqvest.</param>
    /// <param name="status">The current status indicating if the system is active or not.</param>
    /// <param name="busy">A flag indicating if the system is currently busy or idle.</param>
    /// <returns>A ResponseResultBrakeTime object containing the evaluation results.</returns>
    /// 

    private Task<ResponseResultBrakeTime> FetchExistingBrakeTime(object obj, bool status, bool busy)
    {
        if (obj is not TimeDtoReqvest entity)
            return Task.FromResult(new ResponseResultBrakeTime());

        var now = DateTime.Now;

        Console.WriteLine($"IsStartTimeValid: {IsStartTimeValid(entity, status, busy)}");
        Console.WriteLine($"EndTimeLastMinute: {IsLastMinute(entity.EndTime?.ToList(), now)}");
        Console.WriteLine($"StartTimeTimeLastMinute: {IsLastMinute(entity.StartTime?.ToList(), now)}");
        Console.WriteLine($"OnlineTimeDateDay: {HasOnlineTimeToday(entity, now) && !status}");
        Console.WriteLine($"StartTimeBreak: {IsStartTimeBreak(entity, now)}");
        Console.WriteLine($"OfflineTimeDateDay: {HasOfflineTimeToday(entity, now)}");
        Console.WriteLine($"workSchedulPingLog: {ShouldUpdateWorkSchedule(entity, status, busy, now)}");


        return Task.FromResult(new ResponseResultBrakeTime
        {
            StartTimeValidWorkSchedule = IsStartTimeValid(entity, status, busy),
            EndTimeLastMinute = IsLastMinute(entity.EndTime?.ToList(), now),
            StartTimeTimeLastMinute = IsLastMinute(entity.StartTime?.ToList(), now),
            OnlineTimeDateDay = HasOnlineTimeToday(entity, now) && !status,
            StartTimeBreak = IsStartTimeBreak(entity, now),
            OfflineTimeDateDay = HasOfflineTimeToday(entity, now),
            workSchedulPingLog = ShouldUpdateWorkSchedule(entity, status, busy, now)
        });
    }


    private static bool IsStartTimeValid(TimeDtoReqvest entity, bool status, bool busy)
    => entity.StartTime?.Any() == true && status && busy;

    private static bool IsLastMinute(List<DateTime>? times, DateTime now)
    => times?.Any() == true && times.Last().Minute == now.Minute;

    private static bool HasOnlineTimeToday(TimeDtoReqvest entity, DateTime now)
    => entity.OnlineTime?.Any(day => day.Day == now.Day) == true;

    private static bool IsStartTimeBreak(TimeDtoReqvest entity, DateTime now)
     => entity.StartTime?.Any(day => day.Day == now.Day) == false;

    private static bool HasOfflineTimeToday(TimeDtoReqvest entity, DateTime now)
    => entity.OflineTime?.Any(day => day.Day == now.Day) == true;

    private static bool ShouldUpdateWorkSchedule(TimeDtoReqvest entity, bool status, bool busy, DateTime now)
    => !status && entity.StartTime?.Any() == true && !HasOfflineTimeToday(entity, now) && !busy;



    /*
        private Task<ResponseResultBrakeTime> FetchExistingBrakeTime(object obj, bool status, bool busy)
        {
            if (obj is not TimeDtoReqvest entity)
            return Task.FromResult(new ResponseResultBrakeTime());

            var now = DateTime.Now;
            var hasStartTime = entity.StartTime?.Any() == true;
            var hasEndTime = entity.EndTime?.Any() == true;
            var hasOnlineToday = entity.OnlineTime?.Any(day => day.Day == now.Day) == true;
            var hasOfflineToday = entity.OflineTime?.Any(day => day.Day == now.Day) == true;
            var lastMinute = now.Minute;

            return Task.FromResult(new ResponseResultBrakeTime
            {
                StartTimeValidWorkSchedule = hasStartTime && status && busy,
                EndTimeLastMinute = hasEndTime && entity.EndTime!.Last().Minute == lastMinute,
                StartTimeTimeLastMinute = hasStartTime && entity.StartTime!.Last().Minute == lastMinute,
                OnlineTimeDateDay = hasOnlineToday && !status,
                StartTimeBreak = !hasStartTime || !entity.StartTime!.Any(day => day.Day == now.Day),
                OfflineTimeDateDay = hasOfflineToday,
                workSchedulPingLog = !status && hasStartTime && !hasOfflineToday && !busy
            });
        }

    /*
        /*
            private Task<ResponseResultBrakeTime> FetchExistingBrakeTime(object obj, bool status, bool busy)
            {
                var entity = obj as TimeDtoReqvest;

                var result = Task.FromResult(new ResponseResultBrakeTime
                {

                    StartTimeValidWorkSchedule = entity != null && entity.StartTime != null &&
                    entity.StartTime.Any() && status && busy == true,

                    EndTimeLastMinute = entity?.EndTime != null && entity.EndTime.Any() && entity.EndTime.Last().Minute == DateTime.Now.Minute,
                    StartTimeTimeLastMinute = entity?.StartTime != null && entity.StartTime.Any() && entity.StartTime.Last().Minute == DateTime.Now.Minute,
                    OnlineTimeDateDay = (entity?.OnlineTime?.Any(day => day.Day == DateTime.Now.Day) ?? false) && !status,
                    //  OnlineTimeDateDay = (entity?.OnlineTime?.Any() ?? false) && !status,??????
                    StartTimeBreak = entity?.StartTime?.Any(day => day.Day == DateTime.Now.Day) == false,
                    OfflineTimeDateDay = entity?.OflineTime?.Any(day => day.Day == DateTime.Now.Day) ?? false,
                    workSchedulPingLog = !status && entity?.StartTime?.Any() == true &&
                    entity?.OflineTime?.Any(day => day.Day == DateTime.Now.Day) == false && !busy



                });

                return result;
            } */

    /// <summary>
    /// Evaluates and creates a ResponseResultTimeInTimeOut object based on the provided TimeDtoReqvest data.
    /// </summary>
    /// <param name="obj">The object containing time-related data. Expected to be of type TimeDtoReqvest.</param>
    /// <param name="status">The current status indicating if the system is active or not.</param>
    /// <param name="busy">A flag indicating if the system is currently busy or idle.</param>
    /// <returns>A ResponseResultTimeInTimeOut object containing the evaluation results.</returns>


    private Task<ResponseResultTimeInTimeOut> FetchExistingComingAndgoing(object obj, bool status, bool busy)
    {

        return Task.FromResult(new ResponseResultTimeInTimeOut());
    }
}

