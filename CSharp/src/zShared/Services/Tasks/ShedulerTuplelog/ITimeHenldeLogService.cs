
using zShared.Services.Tasks.ShedulerTuplelog.Dto;
using zShared.Services.Tasks.ShedulerTuplelog.Enum;

namespace zShared.Services.Tasks.ShedulerTuplelog
{
    public interface ITimeHenldeLogService
    {
        Task<object> GetTimeResult(TimeDto entity, bool status, bool busy, ServiceResponseType responseType);
    }
}