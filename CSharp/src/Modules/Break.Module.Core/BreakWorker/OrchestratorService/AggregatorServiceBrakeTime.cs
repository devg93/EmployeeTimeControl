using System;
using System.Threading.Tasks;
using Modules.Break.Module.Core.Astractions.Dbcontracts;
using Modules.Break.Module.Core.Astractions.Iservices;
using Modules.Break.Module.Core.Dto;
using Shared.Dto;
using Shared.Mediator;
using Shared.Services.Tasks.ShedulerTuplelog;

namespace Break.Module.Core.BreakWorker.OrchestratorService
{
  public enum ServiceReqvuestType
  {
    ComingAndgoing
  }

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

      var exsitTimeInTimeOut = await RepositoryContract.getServiceTimeInTimeOut.GetByIdAsync();


      TupleReqvestDto tupleReqvestDto = new TupleReqvestDto()
      {
        //   EndTime = entity.EndTime,
        //   StartTime = entity.StartTime,
        //   OnlineTime = entity.OnlineTime,
        //   OflineTime = entity.OflineTime

      };

      TimeDtoReqvest timeDtoReqvest = new TimeDtoReqvest();

      var resultBusy = await RepositoryContract.busyRepositoryQeury.GetBusyByIdAsync(entity.Id);

      // var resultTimne = await mediatorGetService.TimeHandleLogService.GetTimeResult
      // (timeDtoReqvest,Status,resultBusy,ServiceReqvuestType.ComingAndgoing);



      return true;

    }

  }

}