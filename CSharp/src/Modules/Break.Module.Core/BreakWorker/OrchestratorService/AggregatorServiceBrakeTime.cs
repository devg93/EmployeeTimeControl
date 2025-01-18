using System;
using System.Linq;
using System.Threading.Tasks;
using Modules.Break.Module.Core.Astractions.Dbcontracts;
using Modules.Break.Module.Core.Astractions.Iservices;
using Modules.Break.Module.Core.Dto;
using Modules.Break.Module.Core.Entity;
using Shared.Dto;
using Shared.Mediator;
using Shared.Records;
using Shared.Services.Tasks.ShedulerTuplelog.Enum;


namespace Break.Module.Core.BreakWorker.OrchestratorService;

  public class AggregatorServiceBrakeTime : IAggregatorServiceBrakeTime
  {
    private readonly IRepositoryContract RepositoryContract;
    private readonly IMediatorGetService mediatorGetService;

    public AggregatorServiceBrakeTime(IRepositoryContract RepositoryContract, IMediatorGetService mediatorGetService)
    => (this.RepositoryContract, this.mediatorGetService) = (RepositoryContract, mediatorGetService);

    public async Task<bool> AddOrUpdateBrakeTime(BrakeTimeDtoReqvest entity, bool Status)
    {
      if (entity is null) throw new ArgumentNullException(nameof(entity));

      var exsitBrake = await RepositoryContract.brakeTimeRepositoryQeury.GetBreakByIdAsinc(entity.Id);

      var exsitTimeInTimeOut = await RepositoryContract.getServiceTimeInTimeOut.GetByIdAsync(entity.Id);


      TimeDtoReqvest timeDtoReqvest = new TimeDtoReqvest()
      {
        EndTime = exsitBrake.EndTime?.Select(schedule => schedule.dateTime).ToList(),
        StartTime = exsitBrake.StartTime?.Select(schedule => schedule.dateTime).ToList(),
        OnlineTime = exsitTimeInTimeOut.OnlineTime?.Select(schedule => schedule.TimeIn).ToList(),
        OflineTime = exsitTimeInTimeOut.OflineTime?.Select(schedule => schedule.TimeOut).ToList()
      };


      var resultBusy = await RepositoryContract.busyRepositoryQeury.GetBusyByIdAsync(entity.Id);

      ResponseResultBrakeTime resultTimne = (ResponseResultBrakeTime)await mediatorGetService.TimeHandleLogService.GetTimeResult
      (timeDtoReqvest, Status, true, ServiceResponseType.ComingAndgoing);

      try
      {
        {
          if (resultTimne.StartTimeValidWorkSchedule && !resultTimne.OfflineTimeDateDay)
          {

            exsitBrake?.EndTime?.Add(new DateTimeWorkSchedule
            {
              dateTime = DateTime.Now
            });
            await RepositoryContract.busyRepositoryCommand.UpdateBusy(entity.Id, false);
            return await RepositoryContract.brakeTimeRepositoryCommand.Save();

          }
          else if (resultTimne.OnlineTimeDateDay && !resultTimne.OfflineTimeDateDay)
          {

            if (resultTimne.workSchedulPingLog)
            {

              BrakeTime brakeTime = new BrakeTime
              {
                Id = entity.Id,
                StartTime = entity.StartTime?.Select
                (dateTime => new DateTimeWorkSchedule { dateTime = dateTime }).ToList(),
                EndTime = entity.EndTime?.Select
                (dateTime => new DateTimeWorkSchedule { dateTime = dateTime }).ToList()
              };

              await RepositoryContract.brakeTimeRepositoryCommand.CreateBreakAsync(brakeTime);
              await RepositoryContract.busyRepositoryCommand.UpdateBusy(entity.Id, true);
              return true;

            }

            return true;
          }
        }
      }

      catch (Exception ex)
      {
        throw new Exception("Database error occurred while saving changes.", ex.InnerException ?? ex);
      }

      return true;
    }


  }

