
using System.Threading.Tasks;
using Modules.Break.Module.Core.Astractions.Irepository.Ibusy;
using Modules.Break.Module.Core.DAL;
using Modules.Break.Module.Core.Entity;


namespace Modules.Break.Module.Core.Repository.Busy
{
    public class busyRepositoryCommand : IbusyRepositoryCommand
    {
         private readonly DbInstace _db;
      public  busyRepositoryCommand(DbInstace db)
        => _db = db;
        public async Task<bool> CreateBusy(busyChecker entity)
        {
            await _db.AddAsync(entity);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> Save()
        => await _db.SaveChangesAsync() > 0;
        

        public async Task<bool> UpdateBusy(int UserId, bool param)
        {
           
           var entity = await _db.FindAsync<busyChecker>(UserId);
            if (entity != null)
            {
                entity.busy = param;
                _db.Update(entity);
                return await _db.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}