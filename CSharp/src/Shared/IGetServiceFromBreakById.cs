

namespace Shared;

    public interface IGetServiceFromBreakById
    {
         Task<string> GetByIdAsync(int id);
    }
