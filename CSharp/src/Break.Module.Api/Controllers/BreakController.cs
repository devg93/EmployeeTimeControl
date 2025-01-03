using Break.Module.Api.DTO;
using Microsoft.AspNetCore.Mvc;
using zShared.Dto;
using zShared.Mediator;

namespace Break.Module.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreakController : ControllerBase
    {
        private readonly IMediatorGetService _mediatorGetService;

        public BreakController(IMediatorGetService mediatorGetService)
        {
            _mediatorGetService = mediatorGetService;
        }




        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BrakeTimeDto brakeTimeDto)
        {
            // if (brakeTimeDto == null)
            // {
            //     return BadRequest("BrakeTimeDto cannot be null");
            // }

            // var breakRepository = _mediatorGetService.GetBreakRepository;

            // if (breakRepository == null)
            // {
            //     return StatusCode(500, "Break repository is not available");
            // }

            // await breakRepository.CreateBreakAsync(brakeTimeDto);

            // return Ok(true);

            return Ok("Hello World");
        }
    }
}
