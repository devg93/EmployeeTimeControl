
namespace Modules.Break.Module.Core.Repository.Busy.DAL
{
    public class busyRepositoryQeury : IbusyRepositoryQeury
    {
        private readonly DbInstace _db;
      public  busyRepositoryQeury(DbInstace db)
      => _db = db;

        public async Task<bool> GetBusyByIdAsync(int id)
        {
            var res = _db.BusyCheckers != null ?
            await _db.BusyCheckers.FirstOrDefaultAsync(bs => bs.Id == id) : null;
            return res?.busy ?? false;
        }

        public async Task<bool> GetBusyCount(int id)
        {
            if (_db.BusyCheckers != null)
            {
                return await Task.FromResult(_db.BusyCheckers.Count(bs => bs.Id == id) > 0);
            }
            return false;
        }
    }
}