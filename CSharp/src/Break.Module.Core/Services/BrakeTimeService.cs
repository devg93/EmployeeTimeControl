using System;
using System.Threading.Tasks;
using Break.Module.Core.Astractions.Dbcontracts;
using Break.Module.Core.Astractions.Irepository;
using Break.Module.Core.Astractions.Iservices;
using Break.Module.Core.Dto;
using Break.Module.Core.Factory;
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


            /// <summary>
            /// get data module TimeInTimeOut
            /// </summary>
            /// 
            /// <returns>
            /// <see cref="TimeInTimeOutDtoResponse"/> object
            /// </returns>



            /// <summary>
            /// <see cref=" var exitdataBreak = await RepositoryContract.brakeTimeRepositoryCommand.CreateBreakAsync(int id);"/> object
            /// </summary>
            /// 

            /// <summary>
            /// <see cref="  await RepositoryContract.busyRepositoryQeury.GetBusyByIdAsync(1);"/> object
            /// </summary>
            /// <returns> bool </returns>
         
      
            return true;
        }

    }
}