
using Microsoft.AspNetCore.Mvc;
using Shared.Dto;
using Shared.Services.ModuleCommunication;
using TimeInTimeOut.Module.Core.Abstractions;
using TimeInTimeOut.Module.Core.Domain.Entity;
using TimeInTimeOut.Module.Core.Dto;

namespace TimeInTimeOut.Module.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeInTimeOutiController : ControllerBase
    {
        private readonly IcomingAndgoingRepository icomingAndgoingRepository;
        public TimeInTimeOutiController(IcomingAndgoingRepository icomingAndgoingRepository)
        =>this.icomingAndgoingRepository=icomingAndgoingRepository;

        [HttpGet]
        public async Task<ResponseChecker<ComingAndgoingResponseDto>> Get(int Id)
        {
            var comingAndgoingResponseDto =await icomingAndgoingRepository.GetById(Id);

          ResponseChecker<ComingAndgoingResponseDto> comingAndgoingResponseDto1= 
          RuntimeObjectMapper.MapObjectGeneric<ResponseChecker<ComingAndgoingResponseDto>, 
          ComingAndgoingResponseDto>(comingAndgoingResponseDto);
          return comingAndgoingResponseDto1;
        }


        [HttpPost]
        public async Task<IActionResult> Post(ComingAndgoingDtoRequest entity)
        {
          
          var comingAndgoing1 = new ComingAndgoing {
            Id=entity.Id,
          
            OfflineTime = new List<DateTimeTimeInTimeOut> { new DateTimeTimeInTimeOut { TimeIn = entity.OflineTime } },
            // OnlineTime=new List<DateTimeTimeInTimeOut> { new DateTimeTimeInTimeOut { TimeOut = entity.OflineTime } },

            };

             await icomingAndgoingRepository.Add(comingAndgoing1);
             return Ok("insterted");
        }
    }
}