
using System;
using System.Linq;
using System.Threading.Tasks;
using Modules.Break.Module.Core;
using Modules.Break.Module.Core.Astractions.Dbcontracts;
using Modules.Break.Module.Core.Astractions.Iservices;
using Modules.Break.Module.Core.Dto;
using Modules.Break.Module.Core.Entity;
using Shared.Dto;
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
    private readonly IRepositoryContract _repositoryContract;
    private readonly ITimeHenldeLogService _timeHenldeLogService;

    public AggregatorServiceBrakeTime(IRepositoryContract repositoryContract, ITimeHenldeLogService timeHenldeLogService)
    => (_repositoryContract, _timeHenldeLogService) = (repositoryContract, timeHenldeLogService);


    public async Task<bool> AddOrUpdateBrakeTime(BrakeTimeDtoReqvest entity, bool IpStatus)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var existingBrake = await FetchExistingBrakeTime(1); //entity.Id
        var existingTimeInOut = await FetchServiceTimeInTimeOut(1);//entity.Id
        bool BusyStatus = await GetBusyStatus(1);


        if (existingBrake is null || existingTimeInOut is null)
            throw new InvalidOperationException("Required data could not be retrieved from the repository.");


        var timeDto = PrepareTimeDto(existingBrake, existingTimeInOut);

        var response = await _timeHenldeLogService.GetTimeResult(timeDto, IpStatus, BusyStatus, ServiceResponseType.ComingAndgoing);
        var resultTime = (ResponseResultBrakeTime)response;

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

    private async Task<BrakeTime?> FetchExistingBrakeTime(int id)
    {
        return await _repositoryContract.brakeTimeRepositoryQeury.GetBreakByIdAsinc(id);
    }

    private async Task<ComingAndGoingDto?> FetchServiceTimeInTimeOut(int id)
    {
        return await _repositoryContract.getServiceTimeInTimeOut.GetByIdAsync(id);
    }

    private TimeDtoReqvest PrepareTimeDto(BrakeTime existingBrake, ComingAndGoingDto existingTimeInOut)
    {
        return new TimeDtoReqvest
        {
            StartTime = existingBrake.StartTime?.Select(s => s.dateTime).ToList(),
            EndTime = existingBrake.EndTime?.Select(e => e.dateTime).ToList(),
            OnlineTime = existingTimeInOut.OnlineTime?.Select(o => o.TimeIn).ToList(),
            OflineTime = existingTimeInOut.OflineTime?.Select(o => o.TimeOut).ToList()
        };
    }

    private async Task<bool> HandleValidWorkSchedule(BrakeTime existingBrake, int id)
    {
        existingBrake.EndTime?.Add(new DateTimeWorkSchedule { dateTime = DateTime.Now });
        return await UpdateBusyStatus(id, false);
    }

    private async Task<bool> HandleOnlineTimeValid(ResponseResultBrakeTime resultTime, BrakeTimeDtoReqvest entity)
    {
        if (resultTime.workSchedulPingLog)
        {
            var newBrakeTime = new BrakeTime
            {
                Id = entity.Id,
                StartTime = entity.StartTime?.Select(t => new DateTimeWorkSchedule { dateTime = t }).ToList(),
                EndTime = entity.EndTime?.Select(t => new DateTimeWorkSchedule { dateTime = t }).ToList()
            };

            await _repositoryContract.brakeTimeRepositoryCommand.CreateBreakAsync(newBrakeTime);
            return await UpdateBusyStatus(entity.Id, true);
        }

        return true;
    }

    private async Task<bool> UpdateBusyStatus(int id, bool status)
    {
        await _repositoryContract.busyRepositoryCommand.UpdateBusy(id, status);
        return await _repositoryContract.brakeTimeRepositoryCommand.Save();
    }

    private async Task<bool> GetBusyStatus(int Userid)
    {
        var busyChecker = await _repositoryContract.busyRepositoryQeury.GetBusyByIdAsync(Userid);
        return true;
    }

    //***************************************************************************************************************************//
}
