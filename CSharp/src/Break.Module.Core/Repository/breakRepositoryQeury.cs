
using System.Collections.Generic;
using System.Threading.Tasks;
using Break.Module.Core.Astractions.Irepository;
using Break.Module.Core.DLA;
using Break.Module.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace Break.Module.Core.Repository
{
    public class breakRepositoryQeury : IbreakRepositoryQeury
    {
        private readonly DbInstace dbcontext;
        public breakRepositoryQeury(DbInstace dbcontext)
        => this.dbcontext = dbcontext;
        public async Task<List<BrakeTime>> GetAllBreaksAsync()
        => dbcontext.BrakeTimes != null ? await dbcontext.BrakeTimes.Include(ws => ws.EndTime).
        Include(ws => ws.StartTime).ToListAsync() : new List<BrakeTime>();

        public async Task<BrakeTime> GetBreakByIdAsinc(int Id)
        {
            if (dbcontext.BrakeTimes == null)
            {
                return new BrakeTime();
            }

            var brakeTime = await dbcontext.BrakeTimes
                .Include(ws => ws.EndTime)
                .Include(ws => ws.StartTime)
                .FirstOrDefaultAsync(x => x.Id == Id);

            return brakeTime ?? new BrakeTime();
        }
    }
}