
using Break.Module.Core.Astractions;
using zShared.Services;

namespace zShared.Mediator
{
    public interface IMediatorGetService
    {
        //  public IbreakRepository? GetBreakRepository { get; }

        public IPingIpChecker? GetPingIpChecker { get; }
    }
}