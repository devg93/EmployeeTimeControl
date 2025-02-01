
using System.Threading.Tasks;

using Modules.Break.Module.Core.Entity;

namespace Modules.Break.Module.Core.Astractions.Irepository;

    public interface IbreakRepositoryCommand
    {
    
        public Task<string> CreateBreakAsync(BrakeTime brakeTime);
        public Task<string> UbdateBreakAsync(int Id);
        public Task<bool>Save();

    }
