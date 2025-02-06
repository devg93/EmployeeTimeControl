
using Microsoft.AspNetCore.Mvc;
using Shared.Dto;
using Shared.Services.ModuleCommunication;
using Shared.Services.RunTime;
using TimeInTimeOut.Module.Core.Abstractions;
using TimeInTimeOut.Module.Core.Domain.Entity;
using TimeInTimeOut.Module.Core.Dto;

namespace TimeInTimeOut.Module.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeInTimeOutiController : ControllerBase
    {
        private readonly IcomingAndgoingRepositoryCommand icomingAndgoingRepository;
        private readonly IcomingAndgoingRepositoryQeury icomingAndgoingRepositoryQeury;
        public TimeInTimeOutiController(IcomingAndgoingRepositoryCommand icomingAndgoingRepository,
        IcomingAndgoingRepositoryQeury icomingAndgoingRepositoryQeury)
        =>(this.icomingAndgoingRepository,this.icomingAndgoingRepositoryQeury)=
        (icomingAndgoingRepository,icomingAndgoingRepositoryQeury);

        [HttpGet]
        public async Task<ResponseChecker<ComingAndgoingResponseDto>> Get(int Id)
        {
            var comingAndgoingResponseDto =await icomingAndgoingRepositoryQeury.GetById(Id,"OnlineTime");

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
          
            OnlineTime = new List<DateTime> { entity.OnlineTime },
            // OnlineTime=new List<DateTimeTimeInTimeOut> { new DateTimeTimeInTimeOut { TimeOut = entity.OflineTime } },

            };

             await icomingAndgoingRepository.Add(comingAndgoing1);
             return Ok("insterted");
        }
    }
}