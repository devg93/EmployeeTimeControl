using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Break.Module.Core.Astractions;
using Break.Module.Core.DLA;
using Break.Module.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace Break.Module.Core.Repository
{
    public class BreakRepository : IbreakRepository
    {
        private readonly DbInstace dbInstace;
        public BreakRepository(DbInstace dbInstace) => this.dbInstace = dbInstace;

        public async Task<string> CreateBreakAsync(BrakeTime brakeTime)
        {
            await dbInstace.AddAsync(brakeTime);
            await dbInstace.SaveChangesAsync();
            return "Break created";
        }


        public async Task<List<BrakeTime>> GetAllBreaksAsync()
        => dbInstace.BrakeTimes != null ? await dbInstace.BrakeTimes.Include(ws => ws.EndTime).
        Include(ws => ws.StartTime).ToListAsync() : new List<BrakeTime>();



        public async Task<BrakeTime> GetBreakByIdAsinc(int Id)
        {
            if (dbInstace.BrakeTimes == null)
            {
                return new BrakeTime();
            }

            var brakeTime = await dbInstace.BrakeTimes
                .Include(ws => ws.EndTime)
                .Include(ws => ws.StartTime)
                .FirstOrDefaultAsync(x => x.Id == Id);

            return brakeTime ?? new BrakeTime();
        }
    }
}