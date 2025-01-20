

namespace Shared.Services.ModuleCommunication.Contracts;

    public interface IGetServiceFromBreakById
    {
         Task<string> GetByIdAsync(int id);
    }
