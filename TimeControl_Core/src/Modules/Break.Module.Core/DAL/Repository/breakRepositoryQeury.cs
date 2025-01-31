
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Modules.Break.Module.Core.Astractions.Irepository;
using Modules.Break.Module.Core.DAL;
using Modules.Break.Module.Core.Entity;
using Shared.Dto;

namespace Modules.Break.Module.Core.Repository.DAL;

public class breakRepositoryQeury : IbreakRepositoryQeury
{
    private readonly DbInstace dbcontext;
    public breakRepositoryQeury(DbInstace dbcontext)
    => this.dbcontext = dbcontext;
    public async Task<List<BrakeTime>> GetAllBreaksAsync()
    => dbcontext.BrakeTimes != null ?
    await dbcontext.BrakeTimes.Include(ws => ws.BrakeEndTime).
    Include(ws => ws.BrakeStartTime).Include(bs => bs.busyChecker)
    .ToListAsync() : new List<BrakeTime>();

    public async Task<ResponseChecker<BrakeTime>> GetBreakByIdAsinc(int Id)
    {
        if (dbcontext.BrakeTimes is null)
        {
            return new ResponseChecker<BrakeTime>
            {
                IsSuccess = false,
                Message = "Database instance or BrakeTime collection is null."
            };
        }

        var brakeTime = await dbcontext.BrakeTimes
            .Include(ws => ws.BrakeEndTime)
            .Include(ws => ws.BrakeStartTime)
            .Include(bs => bs.busyChecker)
            .FirstOrDefaultAsync(x => x.Id == Id);

        return new ResponseChecker<BrakeTime>
        {
            IsSuccess = true,
            Data = brakeTime
        };
    }


}
