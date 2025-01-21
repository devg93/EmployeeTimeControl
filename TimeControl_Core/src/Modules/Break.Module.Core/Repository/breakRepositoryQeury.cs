
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Modules.Break.Module.Core.Astractions.Irepository;
using Modules.Break.Module.Core.DLA;
using Modules.Break.Module.Core.Entity;

namespace Modules. Break.Module.Core.Repository;

    public class breakRepositoryQeury : IbreakRepositoryQeury
    {
        private readonly DbInstace dbcontext;
        public breakRepositoryQeury(DbInstace dbcontext)
        => this.dbcontext = dbcontext;
        public async Task<List<BrakeTime>> GetAllBreaksAsync()
        => dbcontext.BrakeTimes != null ? 
        await dbcontext.BrakeTimes.Include(ws => ws.EndTime).
        Include(ws => ws.StartTime).Include(bs=>bs.busyChecker)
        .ToListAsync() : new List<BrakeTime>();

        public async Task<BrakeTime> GetBreakByIdAsinc(int Id)
        {
            if (dbcontext.BrakeTimes == null)
            {
                return new BrakeTime();
            }

            var brakeTime = await dbcontext.BrakeTimes
                .Include(ws => ws.EndTime)
                .Include(ws => ws.StartTime)
                .Include(bs=>bs.busyChecker)
                .FirstOrDefaultAsync(x => x.Id == Id);

            return brakeTime ?? new BrakeTime();
        }

   
}
