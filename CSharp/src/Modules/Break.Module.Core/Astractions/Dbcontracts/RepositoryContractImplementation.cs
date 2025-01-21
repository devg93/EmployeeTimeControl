
using Modules.Break.Module.Core.Astractions.Irepository;
using Modules.Break.Module.Core.Astractions.Irepository.Ibusy;
using Shared.Services.ModuleCommunication.Contracts;

using Modules.Break.Module.Core.Astractions.Dbcontracts;

namespace Break.Module.Core.Astractions.Dbcontracts
{
    public class RepositoryContractImplementation : IRepositoryContract
    {
        private IbreakRepositoryCommand? brakeTimeRepositoryCommand;
        private IbreakRepositoryQeury? brakeTimeRepositoryQeury;
        private IbusyRepositoryCommand? busyRepositoryCommand;
        private IGetServiceFtomTimeInTimeOutById? getServiceTimeInTimeOut;
        private IbusyRepositoryQeury? busyRepositoryQeury;
       

        IbusyRepositoryQeury IRepositoryContract.busyRepositoryQeury => busyRepositoryQeury ?? throw new();
        IbreakRepositoryCommand IRepositoryContract.brakeTimeRepositoryCommand => brakeTimeRepositoryCommand ?? throw new();
        IbreakRepositoryQeury IRepositoryContract.brakeTimeRepositoryQeury => brakeTimeRepositoryQeury ?? throw new();
        IbusyRepositoryCommand IRepositoryContract.busyRepositoryCommand => busyRepositoryCommand ?? throw new();
        IGetServiceFtomTimeInTimeOutById IRepositoryContract.getServiceTimeInTimeOut => getServiceTimeInTimeOut ?? throw new();
    }

}