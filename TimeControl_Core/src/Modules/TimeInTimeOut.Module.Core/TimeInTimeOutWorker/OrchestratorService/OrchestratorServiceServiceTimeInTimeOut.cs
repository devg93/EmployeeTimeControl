


using TimeInTimeOut.Module.Core.TimeInTimeOutWorker.CommonServices.PersistenceService.Commands;
using TimeInTimeOut.Module.Core.TimeInTimeOutWorker.CommonServices.PersistenceServiceDb;

namespace TimeInTimeOut.Module.Core.TimeInTimeOutWorker.OrchestratorService
{
    public class OrchestratorServiceServiceTimeInTimeOut : IOrchestratorService
    {
    
        private readonly ITimeValidator _timeValidator;
        private readonly IPersistenceServiceDb _persistenceServiceDb;
        public OrchestratorServiceServiceTimeInTimeOut(ITimeValidator timeValidator,IPersistenceServiceDb persistenceServiceDb)
        =>(_timeValidator,_persistenceServiceDb)=(timeValidator,persistenceServiceDb);

        public async Task<bool> UpdateTimeInTimeOut(ComingAndgoingResponseDto entity, bool IpStatus)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.UserId = 1;


          var Res=  await _timeValidator.TimeValidatorService(entity.Id,IpStatus);
         

            switch ( await _timeValidator.GetResultProces(Res))
            {
                case "WriteDataTimeIn":
                    await _persistenceServiceDb.WriteDataTimeIn(new TimeInWriteCommand { Id = entity.UserId, OnlineTime = DateTime.Now });
                    return true;

                case "UpdateListDataTimeIn":
                    await _persistenceServiceDb.WriteDataTimeInUpdate(new TimeUpdateCommand { UserId = entity.UserId, Param = 'O' });
                    return true;

                case "WriteDataTimeOut":
                    await _persistenceServiceDb.WriteDataTimeOut(new TimeOutWriCommands { Id = entity.UserId, OflineTime = DateTime.Now });
                    return true;


                case "UpdateListDataTimeOut":
                    await _persistenceServiceDb.WriteDataTimeOutUpdate(new TimeUpdateCommand { UserId = entity.UserId, Param = 'F' });
                    return true;


                default:
                    return false;
            }


        }

    }

}


public interface IOrchestratorService
{
    Task<bool> UpdateTimeInTimeOut(ComingAndgoingResponseDto entity, bool Status);
}
