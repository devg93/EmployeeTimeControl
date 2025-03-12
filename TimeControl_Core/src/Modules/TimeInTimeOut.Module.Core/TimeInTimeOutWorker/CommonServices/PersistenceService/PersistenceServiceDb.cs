


using TimeInTimeOut.Module.Core.TimeInTimeOutWorker.CommonServices.PersistenceService.Commands;
using TimeInTimeOut.Module.Core.TimeInTimeOutWorker.CommonServices.PersistenceServiceDb.Queries;


namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.CommonServices.PersistenceServiceDb;

public class PersistenceServiceDb:IPersistenceServiceDb
{
  private readonly IMediator _mediator;
  private readonly ISendServiceToTimeInTimeOutModule _GetdServiceToTimeInTimeOutModule;
  public PersistenceServiceDb(IMediator mediator, ISendServiceToTimeInTimeOutModule GetdServiceToTimeInTimeOutModule)
  => (_mediator, _GetdServiceToTimeInTimeOutModule) = (mediator, GetdServiceToTimeInTimeOutModule);

  public async Task<bool> WriteDataTimeOut(TimeOutWriCommands timeOutWriCommands)
  => await _mediator.Send(timeOutWriCommands);
  public async Task<bool> WriteDataTimeIn(TimeInWriteCommand timeInWriteCommand)
  => await _mediator.Send(timeInWriteCommand);
  public async Task<bool> WriteDataTimeInUpdate(TimeUpdateCommand timeInUpdateCommand)
  => await _mediator.Send(timeInUpdateCommand);
  public async Task<bool> WriteDataTimeOutUpdate(TimeUpdateCommand timeInUpdateCommand)
  => await _mediator.Send(timeInUpdateCommand);
  public async Task<ComingAndgoingResponseDto> GetDataFromBreak(int UserId)
  => await _mediator.Send(new ComingAndgoingQeuries { Id = UserId });
  public async Task<ResponseChecker<BrakeTimeDto>> GetDataFromTimeInTimeOut(int UserId)
  => await _GetdServiceToTimeInTimeOutModule.GetByIdAsync(UserId);


}

public interface IPersistenceServiceDb
{
  Task<bool> WriteDataTimeOut(TimeOutWriCommands timeOutWriCommands);

  Task<bool> WriteDataTimeIn(TimeInWriteCommand timeInWriteCommand);

  Task<bool> WriteDataTimeInUpdate(TimeUpdateCommand timeInUpdateCommand);
  Task<bool> WriteDataTimeOutUpdate(TimeUpdateCommand timeInUpdateCommand);
  Task<ComingAndgoingResponseDto> GetDataFromBreak(int UserId);
  Task<ResponseChecker<BrakeTimeDto>> GetDataFromTimeInTimeOut(int UserId);


}