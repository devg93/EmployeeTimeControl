
namespace Modules.Break.Module.Core
{
    public record ResponseResultBrakeTime
    {
        public bool StartTimeValidWorkSchedule { get; init; }
        public bool EndTimeLastValidWorkSchedule { get; init; }
        public bool StartTimeBreak { get; init; }
        public bool EndTimeLastMinute { get; init; }
        public bool StartTimeTimeLastMinute { get; init; }
        public bool UserOnlineTimeDateDay{ get; init; }
        public bool UserOfflineTimeDateDay { get; init; }
        public bool workSchedulPingLog { get; init; }

    }
}
