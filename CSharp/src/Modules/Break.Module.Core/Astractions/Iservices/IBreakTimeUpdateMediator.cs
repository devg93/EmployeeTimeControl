using System.Threading.Tasks;

namespace Modules. Break.Module.Core.Iservices;

    public interface IBreakTimeUpdateMediator
    {
        Task<bool> UpdateBreakTimeAsync(int userId, bool pingResponseStatus);
    }
