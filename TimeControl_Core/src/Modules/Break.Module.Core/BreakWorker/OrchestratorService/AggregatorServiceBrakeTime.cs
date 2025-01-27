
using System;
using System.Linq;
using System.Threading.Tasks;
using Modules.Break.Module.Core;
using Modules.Break.Module.Core.Astractions.Irepository;
using Modules.Break.Module.Core.Astractions.Irepository.Ibusy;
using Modules.Break.Module.Core.Astractions.Iservices;
using Modules.Break.Module.Core.Dto;
using Modules.Break.Module.Core.Entity;
using Shared.Dto;
using Shared.Services.ModuleCommunication.Contracts;
using Shared.Services.Tasks.PingCheker;
using Shared.Services.Tasks.ShedulerTuplelog;
using Shared.Services.Tasks.ShedulerTuplelog.Enum;


//************************************ Service Orchestration ******************************************//
// The AggregatorServiceBrakeTime class is a central component for coordinating the management of brake time data. 
// It orchestrates the interactions between repositories witch DI modules 
// in updating or creating brake time records. The class implements the following key responsibilities:
// . Validates input data and retrieves necessary information from repositories.



namespace Break.Module.Core.BreakWorker.OrchestratorService;

public class AggregatorServiceBrakeTime : IAggregatorServiceBrakeTime
{
    private readonly IbreakRepositoryCommand breakRepositoryCommand;
    private readonly IbreakRepositoryQeury breakRepositoryQeury;
    private readonly ITimeHenldeLogService timeHenldeLogService;
    private readonly IbusyRepositoryCommand busyRepositoryCommand;
    private readonly IbusyRepositoryQeury busyRepositoryQeury;
    private readonly ISendServiceToBreakModule GetServiceToTimeInTimeOutModule;

    public AggregatorServiceBrakeTime(IbreakRepositoryCommand ibreakRepositoryCommand,
    ITimeHenldeLogService timeHenldeLogService, IbreakRepositoryQeury ibreakRepositoryQeury,
    ISendServiceToBreakModule sendServiceToTimeInTimeOutModule,
    IbusyRepositoryCommand ibusyRepositoryCommand, IbusyRepositoryQeury ibusyRepositoryQeury)
    => (this.breakRepositoryCommand, this.timeHenldeLogService, this.breakRepositoryQeury, this.GetServiceToTimeInTimeOutModule,
    this.busyRepositoryCommand, this.busyRepositoryQeury) =
    (ibreakRepositoryCommand, timeHenldeLogService, ibreakRepositoryQeury,
     sendServiceToTimeInTimeOutModule, ibusyRepositoryCommand, ibusyRepositoryQeury);


    public async Task<bool> AddOrUpdateBrakeTime(BrakeTimeDtoReqvest entity, bool IpStatus)
    {

        var existingTimeInOutResponse = await FetchServiceTimeInTimeOut(1);//entity.Id
        var existingTimeInOut = existingTimeInOutResponse.Data;
        if (existingTimeInOutResponse.IsSuccess is false) return false;



        var existingBrakeResponse = await FetchExistingBrakeTime(1); //entity.Id
        var existingBrake = existingBrakeResponse.Data;
        bool BusyStatus = await GetBusyStatus(1);


        #pragma warning disable CS8604 
        TimeDtoReqvest timeDto = PrepareTimeDto(existingBrake, existingTimeInOut);
        #pragma warning restore CS8604 

        ResponseResultBrakeTime resultTime = (ResponseResultBrakeTime)
        await timeHenldeLogService.GetTimeResult(timeDto, IpStatus, BusyStatus, ServiceResponseType.ComingAndgoing);

        try
        {

            if (resultTime.StartTimeValidWorkSchedule && !resultTime.OfflineTimeDateDay)
            {
                return await HandleValidWorkSchedule(existingBrake, entity.Id);
            }
            else if (resultTime.OnlineTimeDateDay && !resultTime.OfflineTimeDateDay)
            {
                return await HandleOnlineTimeValid(resultTime, entity);
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
    =>await breakRepositoryQeury.GetBreakByIdAsinc(id);
    

    private async Task<ResponseChecker<ComingAndGoingDto>> FetchServiceTimeInTimeOut(int id)
    => await GetServiceToTimeInTimeOutModule.GetByIdAsync(id);
    

    private TimeDtoReqvest PrepareTimeDto(BrakeTime existingBrake, ComingAndGoingDto existingTimeInOut)
    => new TimeDtoReqvest
        {
            StartTime = existingBrake.StartTime?.Select(s => s.StartTime).ToList(),
            EndTime = existingBrake.EndTime?.Select(e => e.EndTime).ToList(),
            OnlineTime = existingTimeInOut.OnlineTime?.Select(o => o.TimeIn).ToList(),
            OflineTime = existingTimeInOut.OflineTime?.Select(o => o.TimeOut).ToList()
        };
    

    private async Task<bool> HandleValidWorkSchedule(BrakeTime existingBrake, int id)
    {
        existingBrake.EndTime?.Add(new DateTimeWorkSchedule { EndTime = DateTime.Now });
        await breakRepositoryCommand.Save();
        return await UpdateBusyStatus(id, false);
    }


    private async Task<bool> HandleOnlineTimeValid(ResponseResultBrakeTime resultTime, BrakeTimeDtoReqvest entity)
    {
        if (resultTime.workSchedulPingLog)
        {
            var newBrakeTime = new BrakeTime
            {
                Id = entity.Id,
                StartTime = entity.StartTime?.Select(t => new DateTimeWorkSchedule { StartTime = t }).ToList(),
                EndTime = entity.EndTime?.Select(t => new DateTimeWorkSchedule { EndTime = t }).ToList()
            };

            await breakRepositoryCommand.CreateBreakAsync(newBrakeTime);
            return await UpdateBusyStatus(entity.Id, true);
        }

        return true;
    }


    private async Task<bool> UpdateBusyStatus(int id, bool status)
    {
        await busyRepositoryCommand.UpdateBusy(id, status);
        return await busyRepositoryCommand.Save();
    }

    private async Task<bool> GetBusyStatus(int Userid)
    {
        var busyChecker = await busyRepositoryQeury.GetBusyByIdAsync(Userid);
        return true;
    }

    //***************************************************************************************************************************//
}
