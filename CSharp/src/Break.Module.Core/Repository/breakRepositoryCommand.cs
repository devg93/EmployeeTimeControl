
using System.Threading.Tasks;
using Break.Module.Core.Astractions.Irepository;
using Break.Module.Core.DLA;
using Break.Module.Core.Entity;

namespace Break.Module.Core.Repository
{
    public class breakRepositoryCommand : IbreakRepositoryCommand
    {
        private readonly DbInstace dbcontext;
        public breakRepositoryCommand(DbInstace dbcontext)
        => this.dbcontext = dbcontext;



        public async Task<string> CreateBreakAsync(BrakeTime brakeTime)
        {
            await dbcontext.AddAsync(brakeTime);
            await dbcontext.SaveChangesAsync();
            return "Break created";
        }
    }
}