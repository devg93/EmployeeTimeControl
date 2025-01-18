
using System.Threading.Tasks;
using Modules.Break.Module.Core.Astractions.Irepository.Ibusy;
using Modules.Break.Module.Core.Entity;


namespace Modules. Break.Module.Core.Repository.Busy
{
    public class busyRepositoryCommand : IbusyRepositoryCommand
    {
        public Task<bool> CreateBusy(busyChecker entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateBusy(int UserId,bool param)
        {
            throw new System.NotImplementedException();
        }
    }
}