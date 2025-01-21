
using Microsoft.AspNetCore.Mvc;
using Modules.Break.Module.Core.Astractions.Irepository;
using Modules.Break.Module.Core.Entity;



namespace Modules.Break.Module.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BreakController : ControllerBase
{
    private readonly IbreakRepositoryCommand ibreakRepositoryCommand;
    private readonly IbreakRepositoryQeury ibreakRepositoryQeury;
    public BreakController(IbreakRepositoryCommand ibreakRepositoryCommand,IbreakRepositoryQeury ibreakRepositoryQeury)
    => (this.ibreakRepositoryCommand, this.ibreakRepositoryQeury) = (ibreakRepositoryCommand, ibreakRepositoryQeury);

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] BrakeTime brakeTimeDto)
    {
        

       var BrakeTime= await ibreakRepositoryCommand.CreateBreakAsync(brakeTimeDto);


        return Ok(BrakeTime);
    }

     [HttpGet]
    public async Task<BrakeTime> Get(int id)
    {
        

       var BrakeTime= await ibreakRepositoryQeury.GetBreakByIdAsinc(id);


        return BrakeTime;
    }
}

