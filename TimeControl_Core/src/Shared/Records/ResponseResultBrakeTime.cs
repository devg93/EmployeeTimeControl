

namespace Shared.Records
{
    public record ResponseResultBrakeTime
    {
        public bool StartTimeValidWorkSchedule { get; init; }
        public bool EndTimeLastValidWorkSchedule { get; init; }

        public bool StartTimeBreak { get; init; }
        public bool EndTimeLastMinute { get; init; }
        public bool StartTimeTimeLastMinute { get; init; }
        public bool OnlineTimeDateDay { get; init; }
        public bool OfflineTimeDateDay { get; init; }
        public bool workSchedulPingLog { get; init; }

    }
}
