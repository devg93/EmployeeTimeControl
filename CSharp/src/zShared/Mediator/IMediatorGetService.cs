
using Break.Module.Core.Astractions;

namespace zShared.Mediator
{
    public interface IMediatorGetService
    {
         public IbreakRepository? GetBreakRepository { get; }
    }
}