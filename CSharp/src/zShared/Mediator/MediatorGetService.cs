using Break.Module.Core.Astractions;

namespace zShared.Mediator
{
    public class MediatorGetService : IMediatorGetService
    {
        private readonly IbreakRepository _breakRepository;

        public MediatorGetService(IbreakRepository breakRepository)
        {
            _breakRepository = breakRepository ?? throw new ArgumentNullException(nameof(breakRepository));
        }

        public IbreakRepository? GetBreakRepository => _breakRepository;
    }
}
