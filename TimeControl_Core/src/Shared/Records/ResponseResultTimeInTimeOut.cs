

namespace Shared.Records
{
    public record ResponseResultTimeInTimeOut
    {
        public bool HasOnlineRecordForToday { get; init; }
        public bool HasSufficientTimePassed { get; init; }
        public bool HasOfflineRecordForToday { get; init; }
        public bool LastTimeIn { get; init; }

    }
}
