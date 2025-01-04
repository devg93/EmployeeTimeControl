
using System.Threading.Tasks;
using Break.Module.Core.Entity;

namespace Break.Module.Core.Astractions.Irepository
{
    public interface IbreakRepositoryCommand
    {
    
        public Task<string> CreateBreakAsync(BrakeTime brakeTime);

    }
}