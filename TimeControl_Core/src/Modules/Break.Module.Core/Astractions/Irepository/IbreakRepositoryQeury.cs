
namespace Modules.Break.Module.Core.Astractions.Irepository;

    public interface IbreakRepositoryQeury
    {
         Task<ResponseChecker<BrakeTime>> GetBreakByIdAsinc(int Id);
         Task<List<BrakeTime>> GetAllBreaksAsync();

    }
