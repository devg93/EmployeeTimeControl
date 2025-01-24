
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Modules.Break.Module.Core.Astractions.Irepository;
using Modules.Break.Module.Core.DAL;
using Modules.Break.Module.Core.Entity;


namespace Modules.Break.Module.Core.Repository;

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

    
}
