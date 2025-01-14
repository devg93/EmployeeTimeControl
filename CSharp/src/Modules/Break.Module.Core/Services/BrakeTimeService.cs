using System;
using System.Threading.Tasks;
using Modules.Break.Module.Core.Astractions.Dbcontracts;
using Modules.Break.Module.Core.Astractions.Iservices;
using Modules.Break.Module.Core.Dto;
using Shared.Dto;
using Shared.Services.Tasks.ShedulerTuplelog;

namespace Break.Module.Core.Services
{
    public class BrakeTimeService : IBrakeTimeService
    {
        private readonly IRepositoryContract RepositoryContract;

        public BrakeTimeService(IRepositoryContract RepositoryContract)
        => this.RepositoryContract = RepositoryContract;

        public async Task<bool> addService(BrakeTimeDtoReqvest entity, bool Status)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            var exsitBrake = await RepositoryContract.brakeTimeRepositoryQeury.GetBreakByIdAsinc(entity.Id);

            var exsitTimeInTimeOut = await RepositoryContract.getServiceTimeInTimeOut.GetByIdAsync();
             var tupleHendleLogService = new TimeHenldeLogService();


          TupleReqvestDto tupleReqvestDto = new TupleReqvestDto()
          {
            //   EndTime = entity.EndTime,
            //   StartTime = entity.StartTime,
            //   OnlineTime = entity.OnlineTime,
            //   OflineTime = entity.OflineTime

          };
          var result = await RepositoryContract.busyRepositoryQeury.GetBusyByIdAsync(entity.Id);

      
            return true;
        }

    }
}