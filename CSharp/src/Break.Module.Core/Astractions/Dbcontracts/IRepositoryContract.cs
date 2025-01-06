using Break.Module.Core.Astractions.Irepository;
using Break.Module.Core.Astractions.Irepository.Ibusy;

namespace Break.Module.Core.Astractions.Dbcontracts
{
    public interface IRepositoryContract
    {
        IbreakRepositoryCommand brakeTimeRepositoryCommand { get; }
        IbreakRepositoryQeury brakeTimeRepositoryQeury { get; }
        IbusyRepositoryCommand busyRepositoryCommand { get; }
        IbusyRepositoryQeury busyRepositoryQeury { get; }

    }
}