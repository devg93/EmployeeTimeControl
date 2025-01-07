
using zShared.Services;

namespace zShared.Mediator
{
    public class MediatorGetService : IMediatorGetService
    {
       
        public IPingIpChecker? GetPingIpChecker => throw new NotImplementedException();
    }

    public interface IPingIpChecker
    {
    }
}
