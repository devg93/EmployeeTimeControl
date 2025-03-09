

namespace Modules.Break.Module.Core.Astractions.Irepository;

    public interface IbreakRepositoryCommand
    {
    
         Task<string> CreateBreakAsync(BrakeTime brakeTime);
         Task<string> UbdateBreakAsync(int Id,byte param);
         Task<bool>Save();

    }
