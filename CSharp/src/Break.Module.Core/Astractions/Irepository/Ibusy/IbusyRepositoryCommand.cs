using System.Threading.Tasks;
using Break.Module.Core.Entity;

namespace Break.Module.Core.Astractions.Irepository.Ibusy
{
    public interface IbusyRepositoryCommand
    {
        Task<bool> CreateBusy(busyChecker entity);
        Task<bool> UpdateBusy(busyChecker entity);

    }
}