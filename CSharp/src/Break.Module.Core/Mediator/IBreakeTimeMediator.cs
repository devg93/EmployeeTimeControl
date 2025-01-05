using System.Threading.Tasks;

namespace Break.Module.Core.Mediator
{
    public interface IBreakeTimeMediator
    {
        Task<bool> UpdateAsync(int userId, bool pingResponseStatus);
    }
}