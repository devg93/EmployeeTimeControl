
using System.Threading.Tasks;
using Modules.Break.Module.Core.Entity;


namespace Modules.Break.Module.Core.Astractions.Irepository.Ibusy;

    public interface IbusyRepositoryQeury
    {
         Task<busyChecker> GetBusyByIdAsync(int  id);

    }
