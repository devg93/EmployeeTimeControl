using System.Threading.Tasks;
using Modules.Break.Module.Core.Entity;

namespace Modules.Break.Module.Core.Astractions.Irepository.Ibusy;

    public interface IbusyRepositoryCommand
    {
        Task<bool> CreateBusy(busyChecker entity);
        Task<bool> UpdateBusy(int UserId, bool param);
        Task<bool>Save();

    }
