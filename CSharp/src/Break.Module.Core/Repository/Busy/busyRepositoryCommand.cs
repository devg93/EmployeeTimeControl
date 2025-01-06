
using System.Threading.Tasks;
using Break.Module.Core.Astractions.Irepository.Ibusy;
using Break.Module.Core.Entity;

namespace Break.Module.Core.Repository.Busy
{
    public class busyRepositoryCommand : IbusyRepositoryCommand
    {
        public Task<bool> CreateBusy(busyChecker entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateBusy(busyChecker entity)
        {
            throw new System.NotImplementedException();
        }
    }
}