
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

    public async Task<string> UbdateBreakAsync(int Id, byte param)
    {
        var brakeTime = dbcontext.BrakeTimes != null 
            ? await dbcontext.BrakeTimes.FirstOrDefaultAsync(schedule => schedule.UserId == Id) 
            : null;
        if (brakeTime == null)
        {
            return "Break not found";
        }
       switch (param)
        {
            case 1:
                brakeTime.BrakeStartTime?.Add(DateTime.Now);
                break;
            case 2:
                brakeTime.BrakeEndTime?.Add(DateTime.Now);
                break;
            default:
                return "Invalid parameter";
        }

        await dbcontext.SaveChangesAsync();
        return "Break updated";
    }
}
