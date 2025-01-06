
using System.Threading.Tasks;
using Break.Module.Core.Entity;

namespace Break.Module.Core.Astractions.Irepository.Ibusy
{
    public interface IbusyRepositoryQeury
    {
         Task<busyChecker> GetBusyByIdAsync(int  id);

    }
}