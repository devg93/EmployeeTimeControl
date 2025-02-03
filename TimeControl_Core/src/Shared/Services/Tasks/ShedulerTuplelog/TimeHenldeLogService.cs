
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

                return await FetchExistingComingAndGoing(entity, status, busy);

            default:
                return ("Invalid ServiceResponseType", nameof(responseType));
        }
    }

    //******************************************Private Methods ********************************************************************

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


        return Task.FromResult(new ResponseResultBrakeTime
        {
            UserOnlineTimeDateDay = HasOnlineTimeToday(entity, now),

            UserOfflineTimeDateDay = HasOfflineTimeToday(entity, now),

            StartTimeBreak = IsStartTimeBreak(entity, now),

            StartTimeValidWorkSchedule = IsStartTimeValid(entity, status, busy),

            EndTimeLastMinute = IsLastMinute(entity.EndTime?.ToList(), now),

            StartTimeTimeLastMinute = IsLastMinute(entity.StartTime?.ToList(), now),

            workSchedulPingLog = ShouldUpdateWorkSchedule(entity, status, busy, now)
        });
    }


    private bool IsStartTimeValid(TimeDtoReqvest entity, bool status, bool busy)
    => entity.StartTime?.Any() == true && status && busy;

    private bool IsLastMinute(List<DateTime>? times, DateTime now)
    => times?.Any() == true && times.Last().Minute == now.Minute;

    private bool HasOnlineTimeToday(TimeDtoReqvest entity, DateTime now)
    => entity?.OnlineTime?.Any(day => day.Day == now.Day) ?? false;
    private bool IsStartTimeBreak(TimeDtoReqvest entity, DateTime now)
     => entity?.StartTime?.Any(day => day.Day == now.Day) == false;

    private bool HasOfflineTimeToday(TimeDtoReqvest entity, DateTime now)
    => entity.OflineTime?.Any(day => day.Day == now.Day) ?? false;

    private bool ShouldUpdateWorkSchedule(TimeDtoReqvest entity, bool status, bool busy, DateTime now)
    {

        bool res = HasOfflineTimeToday(entity, now);
        var eny = !status && entity.StartTime?.Any() == true;
        return status && entity.StartTime?.Any() == true && busy;
    }





    /// <summary>
    /// Evaluates and creates a ResponseResultTimeInTimeOut object based on the provided TimeDtoReqvest data.
    /// </summary>
    /// <param name="obj">The object containing time-related data. Expected to be of type TimeDtoReqvest.</param>
    /// <param name="status">The current status indicating if the system is active or not.</param>
    /// <param name="busy">A flag indicating if the system is currently busy or idle.</param>
    /// <returns>A ResponseResultTimeInTimeOut object containing the evaluation results.</returns>


    private Task<ResponseResultTimeInTimeOut> FetchExistingComingAndGoing(object obj, bool status, bool busy)
    {
        if (obj is not TimeDtoReqvest entity)
            return Task.FromResult(new ResponseResultTimeInTimeOut());

        return Task.FromResult(new ResponseResultTimeInTimeOut
        {
            HasOnlineRecordForToday = HasOnlineRecord(entity),
            HasSufficientTimePassed = HasSufficientTimePassed(entity),
            HasOfflineRecordForToday = HasOfflineRecord(entity),
            LastTimeIn = CalculateLastTimeIn(entity, status)
        });
    }

  
    private bool HasOnlineRecord(TimeDtoReqvest entity)
    {
        return entity.OnlineTime?.Any(time => time.Day == DateTime.Now.Day) ?? false;
    }

   
    private bool HasSufficientTimePassed(TimeDtoReqvest entity)
    {
        return entity.OnlineTime?.Any(time => time.Day == DateTime.Now.Day) == true;
    }

   
    private bool HasOfflineRecord(TimeDtoReqvest entity)
    {
        return entity.OflineTime?.Any(time => time.Day == DateTime.Now.Day) ?? false;
    }

   
    private bool CalculateLastTimeIn(TimeDtoReqvest entity, bool status)
    {
        return entity.StartTime != null &&
               entity.OnlineTime?.Any() == true &&
               !status &&
               entity.StartTime.Any() &&
               (DateTime.Now - entity.StartTime.LastOrDefault()).TotalHours >= 3 &&
               !(entity.OflineTime?.Any(time => time.Date == DateTime.Today) ?? false);
    }



}


