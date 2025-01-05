using System.Threading.Tasks;

namespace Break.Module.Core.Mediator
{
    public interface IBreakeTimeUpdate
    {
        Task<bool> UpdateAsync(int userId, bool pingResponseStatus);
    }
}