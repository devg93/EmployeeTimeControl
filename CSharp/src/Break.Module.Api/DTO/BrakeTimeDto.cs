
using zShared.Dto;

namespace Break.Module.Api.DTO
{
    public class BrakeTimeDto : IBrakeTimeDto
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ICollection<DateTimeWorkSchedule>? StartTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ICollection<DateTimeWorkSchedule>? EndTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int? busyId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public busyChecker? busyChecker { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}