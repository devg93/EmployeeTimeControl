
using Microsoft.AspNetCore.Mvc;
using Shared.Dto;
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
        public async Task<ResponseChecker<ComingAndgoing>> Get(int Id)
        {
            return await icomingAndgoingRepository.GetById(Id);
        }


        [HttpPost]
        public async Task<IActionResult> Post(ComingAndgoing entity)
        {
             await icomingAndgoingRepository.Add(entity);
             return Ok("dada");
        }
    }
}