
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Modules.Break.Module.Core;
using Modules.Break.Module.Core.Astractions.Iservices;
using Modules.Break.Module.Core.Dto;
using Modules.Break.Module.Core.Entity;
using Shared.Dto;
using Shared.Services.Tasks.ShedulerTuplelog.Enum;
using Shared.Services.ModuleCommunication;
using Break.Module.Core.Abstraction.IServiceProvider;
using Modules.Break.Module.Core.Astractions.Irepository;
using Shared.Services.ModuleCommunication.Contracts;

//************************************ Service Orchestration ******************************************//
// The AggregatorServiceBrakeTime class is a central component for coordinating the management of brake time data. 
// It orchestrates the interactions between repositories witch DI modules 
// in updating or creating brake time records. The class implements the following key responsibilities:
// . Validates input data and retrieves necessary information from repositories.

namespace Break.Module.Core.BreakWorker.OrchestratorService;

public class AggregatorServiceBrakeTime : IAggregatorServiceBrakeTime
{

    private readonly IServicesFactory Services;
    private  readonly IbreakRepositoryQeury IbreakRepositoryQeury;
    private readonly ISendServiceToBreakModule GetServiceToBreakModule;

    public AggregatorServiceBrakeTime(IServicesFactory brakeServiceAggregatorDiServices, 
    IbreakRepositoryQeury ibreakRepositoryQeury,ISendServiceToBreakModule getServiceToBreakModule)
    {
        Services = brakeServiceAggregatorDiServices;
        IbreakRepositoryQeury = ibreakRepositoryQeury;
        GetServiceToBreakModule = getServiceToBreakModule;
    }


    public async Task<bool> AddOrUpdateBrakeTime(BrakeTimeDtoReqvest entity, bool IpStatus)
    {


        var existingTimeInOutResponse = await FetchServiceTimeInTimeOut(1);//entity.Id
        var existingTimeInOut = existingTimeInOutResponse.Data;
        if (existingTimeInOutResponse.IsSuccess is false) return false;
        var existingBrakeResponse = await FetchExistingBrakeTime(1); //entity.Id
        var existingBrake = existingBrakeResponse.Data;
        bool BusyStatus = await GetBusyStatus(1);



#pragma warning disable CS8604
        var timeDto = PrepareTimeDto(existingBrake, existingTimeInOut);
#pragma warning restore CS8604

        var UserInfo = await Services.GetTimeHandleLogService().GetTimeResult(timeDto, IpStatus, BusyStatus, ServiceResponseType.BrakeTime);

        ResponseResultBrakeTime brakeTimeResult = RuntimeObjectMapper.MapObject<ResponseResultBrakeTime>(UserInfo);
        try
        {

            if (brakeTimeResult.StartTimeValidWorkSchedule && !brakeTimeResult.UserOfflineTimeDateDay && BusyStatus)
            {
                return await HandleValidWorkSchedule(brakeTimeResult, existingBrake, entity.Id, IpStatus);
            }
            else if (brakeTimeResult.UserOnlineTimeDateDay && !brakeTimeResult.UserOfflineTimeDateDay && !BusyStatus && !IpStatus)
            {
                return await HandleOnlineTimeValid(brakeTimeResult, entity, IpStatus);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Database error occurred while saving changes.", ex);
        }

        return true;
    }

    //***************************************Private Async Methods ***********************************************************//

    private async Task<ResponseChecker<BrakeTime>> FetchExistingBrakeTime(int id)
    => await IbreakRepositoryQeury.GetBreakByIdAsinc(id);
    private async Task<ResponseChecker<ComingAndGoingDto>> FetchServiceTimeInTimeOut(int id)
    => await GetServiceToBreakModule.GetByIdAsync(id);
    private TimeDtoReqvest PrepareTimeDto(BrakeTime existingBrake, ComingAndGoingDto existingTimeInOut)
    {
        if (existingTimeInOut is null)
        {
            return new TimeDtoReqvest
            {

                StartTime = existingBrake.BrakeStartTime,
                EndTime = existingBrake.BrakeEndTime,
                OnlineTime = new List<DateTime> { },
                OflineTime = new List<DateTime> { },


            };
        }

        if (existingBrake is null)
        {
            return new TimeDtoReqvest
            {


                StartTime = new List<DateTime> { },
                EndTime = new List<DateTime> { },
                OnlineTime = existingTimeInOut.OnlineTime?.Select(o => o.TimeIn).ToList(),
                OflineTime = existingTimeInOut.OflineTime?.Select(o => o.TimeOut).ToList()


            };
        }


        return new TimeDtoReqvest
        {
            StartTime = existingBrake.BrakeStartTime,
            EndTime = existingBrake.BrakeEndTime,
            OnlineTime = existingTimeInOut.OnlineTime?.Select(o => o.TimeIn).ToList(),
            OflineTime = existingTimeInOut.OflineTime?.Select(o => o.TimeOut).ToList()
        };
    }

    private async Task<bool> HandleValidWorkSchedule(ResponseResultBrakeTime resultTime, BrakeTime existingBrake, int id, bool status)
    {
        if (resultTime.workSchedulPingLog)
        {

            await Services.GetBreakRepositoryCommand().UbdateBreakAsync(1, 2);
            return await UpdateBusyStatus(1, false);
        }
        return false;
    }

    private async Task<bool> HandleOnlineTimeValid(ResponseResultBrakeTime resultTime, BrakeTimeDtoReqvest entity, bool status)
    {

        var newBrakeTime = new BrakeTime
        {
            UserId = entity.UserId,
            BrakeStartTime = entity.StartTime,
            BrakeEndTime = entity.EndTime,

        };
        if (resultTime.StartTimeBreak)
        {


            await Services.GetBreakRepositoryCommand().CreateBreakAsync(newBrakeTime);
            return await CreateBusyStatus(entity.UserId, true);//entity.Id
        }
        await Services.GetBreakRepositoryCommand().UbdateBreakAsync(1, 1);
        return await UpdateBusyStatus(1, true);

    }

    private async Task<bool> UpdateBusyStatus(int id, bool status)
    {
        await Services.GetBusyRepositoryCommand().UpdateBusy(id, status);
        return await Services.GetBusyRepositoryCommand().Save();
    }

    private async Task<bool> GetBusyStatus(int Userid)
    {
        var busyChecker = await Services.GetBusyRepositoryQeury().GetBusyByIdAsync(Userid);
        return busyChecker ? true : false;
    }

    private async Task<bool> CreateBusyStatus(int Userid, bool status)
    {
        var busyChecker = await Services.GetBusyRepositoryCommand().CreateBusy(Userid, status);
        return busyChecker;
    }
    private async Task<bool> GetBusyCount(int Userid)
    {
        var busyChecker = await Services.GetBusyRepositoryQeury().GetBusyCount(1);
        return busyChecker;
    }
    //***************************************************************************************************************************//
}
