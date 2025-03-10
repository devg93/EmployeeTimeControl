
namespace Modules.Break.Module.Core.Astractions.Irepository.Ibusy;

    public interface IbusyRepositoryQeury
    {
         Task<bool> GetBusyByIdAsync(int  id);
         Task<bool> GetBusyCount(int id);

    }
