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

  [HttpGet]
public async Task<IEnumerable<BrakeTimeDto>> Get()
{
    var breakRepository = await _mediatorGetService.GetBreakRepository.GetAllBreaksAsync();

    var breakDtos = breakRepository.Select(b => new BrakeTimeDto
    {
        Id = b.Id,
        StartTime = b.StartTime?.Select(s => new zShared.Dto.DateTimeWorkSchedule
        {
            dateTime = s.dateTime,
            // თუ სხვა თვისებებია საჭირო, აქ დაამატეთ
        }).ToList(),
        EndTime = b.EndTime?.Select(e => new zShared.Dto.DateTimeWorkSchedule
        {
            dateTime = e.dateTime,
            // თუ სხვა თვისებებია საჭირო, აქ დაამატეთ
        }).ToList(),
        busyId = b.busyChecker?.Id,
        busyChecker = b.busyChecker != null ? new zShared.Dto.busyChecker
        {
            Id = b.busyChecker.Id,
            busy = b.busyChecker.busy
        } : null
    });

    return breakDtos;
}


        // [HttpPost]
        // public async Task<IActionResult> Post([FromBody] BrakeTimeDto brakeTimeDto)
        // {
        //     if (brakeTimeDto == null)
        //     {
        //         return BadRequest("BrakeTimeDto cannot be null");
        //     }

        //     var breakRepository = _mediatorGetService.GetBreakRepository;

        //     if (breakRepository == null)
        //     {
        //         return StatusCode(500, "Break repository is not available");
        //     }

        //     await breakRepository.CreateBreakAsync(brakeTimeDto);

        //     return Ok(true);
        // }
    }
}
