
using Break.Module.Api.DTO;
using Microsoft.AspNetCore.Mvc;
using Modules.Break.Module.Core.Astractions.Irepository;
using Modules.Break.Module.Core.Entity;
using Shared.Dto;



namespace Modules.Break.Module.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BreakController : ControllerBase
{
    private readonly IbreakRepositoryCommand ibreakRepositoryCommand;
    private readonly IbreakRepositoryQeury ibreakRepositoryQeury;

    public BreakController(IbreakRepositoryCommand ibreakRepositoryCommand, IbreakRepositoryQeury ibreakRepositoryQeury)
    => (this.ibreakRepositoryCommand, this.ibreakRepositoryQeury) =
    (ibreakRepositoryCommand, ibreakRepositoryQeury);

    [HttpPost]
    public async Task<IActionResult> Post( BrakeTimeReqveust brakeTimeDto)
    {
        var brakeTime = new BrakeTime
        {
            BrakeStartTime = 
                 new List<DateTimeWorkSchedule> { new DateTimeWorkSchedule { StartTime = brakeTimeDto.StartTime } },
               
            // BrakeEndTime = new List<DateTimeWorkSchedule> { new DateTimeWorkSchedule { EndTime = brakeTimeDto.EndTime } },


            // busyId = brakeTimeDto.busyId,
            // busyChecker = brakeTimeDto.busyChecker != null ? new busyChecker
            // {
            //     Id = brakeTimeDto.busyChecker.Id,
            //     busy = brakeTimeDto.busyChecker.busy
            // } : null
        };


        var BrakeTime = await ibreakRepositoryCommand.CreateBreakAsync(brakeTime);


        return Ok(BrakeTime);
    }

    [HttpGet]
    public async Task<ResponseChecker<BrakeTime>> Get(int id)
    {


        var BrakeTime = await ibreakRepositoryQeury.GetBreakByIdAsinc(id);



        return BrakeTime;
    }
}

