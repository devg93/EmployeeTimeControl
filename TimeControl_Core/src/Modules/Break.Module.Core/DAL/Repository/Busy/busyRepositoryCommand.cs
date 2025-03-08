

namespace Modules.Break.Module.Core.Repository.Busy.DAL
{
    public class busyRepositoryCommand : IbusyRepositoryCommand
    {
         private readonly DbInstace _db;
      public  busyRepositoryCommand(DbInstace db)
        => _db = db;
     

        public async Task<bool> CreateBusy(int UserId, bool param)
        {
             var entity = new busyChecker
             {
                Id = UserId,
                busy = param
            };

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