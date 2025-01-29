
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

                var res=await FetchExistingBrakeTime(entity, status, busy);
                res.GetHashCode();
                res.GetType();

                return res;

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
    /// <returns>
    /// A Task that resolves to a ResponseResultBrakeTime object containing the following properties:
    /// - StartTimeValidWorkSchedule: Indicates if the StartTime exists, is valid, and the system is active and busy.
    /// - EndTimeLastMinute: Checks if the EndTime's last entry matches the current minute.
    /// - StartTimeTimeLastMinute: Checks if the StartTime's last entry matches the current minute.
    /// - OnlineTimeDateDay: Indicates if any OnlineTime matches today's date while the system is not active.
    /// - StartTimeBreak: Indicates if there is no StartTime entry for today.
    /// - OfflineTimeDateDay: Indicates if any OfflineTime matches today's date.
    /// - workSchedulPingLog: Evaluates the status and time-related data for logging work schedule.
    /// </returns>


    private async Task<ResponseResultBrakeTime> FetchExistingBrakeTime(object obj, bool status, bool busy)
    {
        var entity = obj as TimeDtoReqvest;

        var result = await Task.FromResult(new ResponseResultBrakeTime
        {

            // StartTimeValidWorkSchedule = entity != null && entity.StartTime != null &&
            // entity.StartTime.Any() && status && busy == true,

            // EndTimeLastMinute = entity?.EndTime != null && entity.EndTime.Any() && entity.EndTime.Last().Minute == DateTime.Now.Minute,
            // StartTimeTimeLastMinute = entity?.StartTime != null && entity.StartTime.Any() && entity.StartTime.Last().Minute == DateTime.Now.Minute,
            // OnlineTimeDateDay = (entity?.OnlineTime?.Any(day => day.Day == DateTime.Now.Day) ?? false) && !status,
            // //  OnlineTimeDateDay = (entity?.OnlineTime?.Any() ?? false) && !status,??????
            // StartTimeBreak = entity?.StartTime?.Any(day => day.Day == DateTime.Now.Day) == false,
            // OfflineTimeDateDay = entity?.OflineTime?.Any(day => day.Day == DateTime.Now.Day) ?? false,
            // workSchedulPingLog = !status && entity?.StartTime?.Any() == true &&
            // entity?.OflineTime?.Any(day => day.Day == DateTime.Now.Day) == false && !busy
            StartTimeValidWorkSchedule = true,
            EndTimeLastMinute = true,
            StartTimeTimeLastMinute = true,
            OnlineTimeDateDay = true,
            StartTimeBreak = true,
            OfflineTimeDateDay = true,
            workSchedulPingLog = false


        });
        Console.WriteLine("result: {0}", result);
        return result;
    }

    private async Task<object> FetchExistingComingAndgoing(object obj, bool status, bool busy)
    {

        return await Task.FromResult(obj);
    }
}

