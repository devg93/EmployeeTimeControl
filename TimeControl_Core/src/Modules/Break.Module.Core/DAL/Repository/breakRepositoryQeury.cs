
namespace Modules.Break.Module.Core.Repository.DAL;

public class breakRepositoryQeury : IbreakRepositoryQeury
{
    private readonly DbInstace dbcontext;
    public breakRepositoryQeury(DbInstace dbcontext)
    => this.dbcontext = dbcontext;
    public async Task<List<BrakeTime>> GetAllBreaksAsync()
    => dbcontext.BrakeTimes != null ?
    await dbcontext.BrakeTimes
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
            
            .FirstOrDefaultAsync(x => x.Id == Id);

        return new ResponseChecker<BrakeTime>
        {
            IsSuccess = true,
            Data = brakeTime
        };
    }


}
