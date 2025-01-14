
using Modules.Break.Module.Core.Astractions.Irepository;
using Modules.Break.Module.Core.Astractions.Irepository.Ibusy;
using Shared;


namespace Modules.Break.Module.Core.Astractions.Dbcontracts;

    public interface IRepositoryContract
    {
        IbreakRepositoryCommand brakeTimeRepositoryCommand { get; }
        IbreakRepositoryQeury brakeTimeRepositoryQeury { get; }
        IbusyRepositoryCommand busyRepositoryCommand { get; }
        IbusyRepositoryQeury busyRepositoryQeury { get; }
        IGetServiceTimeInTimeOut getServiceTimeInTimeOut { get; }

    }
