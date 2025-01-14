

using Shared.Services.Tasks.ShedulerTuplelog.Dto;
using Shared.Services.Tasks.ShedulerTuplelog.Enum;

namespace Shared.Services.Tasks.ShedulerTuplelog;

    public interface ITimeHenldeLogService
    {
        Task<object> GetTimeResult(TimeDtoReqvest entity, bool status, bool busy, ServiceResponseType responseType);
    }
