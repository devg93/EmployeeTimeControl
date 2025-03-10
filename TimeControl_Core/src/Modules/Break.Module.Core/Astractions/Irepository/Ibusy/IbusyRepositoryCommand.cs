
namespace Modules.Break.Module.Core.Astractions.Irepository.Ibusy;

    public interface IbusyRepositoryCommand
    {
        Task<bool> CreateBusy(int UserId, bool param);
        Task<bool> UpdateBusy(int UserId, bool param);
        Task<bool>Save();

    }
