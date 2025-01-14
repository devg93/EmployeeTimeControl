using System.Threading.Tasks;

namespace Modules. Break.Module.Core.Mediator;

    public interface IBreakeTimeMediator
    {
        Task<bool> UpdateAsync(int userId, bool pingResponseStatus);
    }
