
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Modules.Break.Module.Core.Astractions.Irepository;
using Modules.Break.Module.Core.DAL;
using Modules.Break.Module.Core.Entity;


namespace Modules.Break.Module.Core.Repository.DAL;

public class breakRepositoryCommand : IbreakRepositoryCommand
{
    private readonly DbInstace dbcontext;
    public breakRepositoryCommand(DbInstace dbcontext)
    { this.dbcontext = dbcontext;}

    public async Task<string> CreateBreakAsync(BrakeTime brakeTime)
    {
        await dbcontext.AddAsync(brakeTime);
        await dbcontext.SaveChangesAsync();
        return "Break created";
    }

    public async Task<bool> Save()
    => await dbcontext.SaveChangesAsync() > 0;

    public async Task<string> UbdateBreakAsync(int Id)
    {
        var brakeTime = dbcontext.DateTimeWorkSchedules != null 
            ? await dbcontext.DateTimeWorkSchedules.FirstOrDefaultAsync(schedule => schedule.Id == Id) 
            : null;
        if (brakeTime == null)
        {
            return "Break not found";
        }

        if (brakeTime.StartTime != null)
        {
            brakeTime.EndTime = DateTime.Now;
        }

        await dbcontext.SaveChangesAsync();
        return "Break updated";
    }
}
