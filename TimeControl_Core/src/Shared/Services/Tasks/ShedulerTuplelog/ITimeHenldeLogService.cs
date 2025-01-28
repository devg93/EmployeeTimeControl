
using Shared.Dto;
using Shared.Services.Tasks.ShedulerTuplelog.Enum;

namespace Shared.Services.Tasks.ShedulerTuplelog;

    public interface ITimeHenldeLogService
    {
       // Task<IntPtr> GetTimeResult(TimeDtoReqvest entity, bool status, bool busy, ServiceResponseType responseType);
        Task<object> GetTimeResult(TimeDtoReqvest entity, bool status, bool busy, ServiceResponseType responseType);
    }
