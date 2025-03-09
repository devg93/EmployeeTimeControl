
//************************************ Service Orchestration ******************************************//
// The AggregatorServiceBrakeTime class is a central component for coordinating the management of brake time data. 
// It orchestrates the interactions between repositories witch DI modules 
// in updating or creating brake time records. The class implements the following key responsibilities:
// . Validates input data and retrieves necessary information from repositories.

namespace Break.Module.Core.BreakWorker.OrchestratorService;

public class AggregatorServiceBrakeTime : IAggregatorServiceBrakeTime
{

    private readonly IBrakeTimeDataManager _brakeTimeDataManager;
    private readonly IBrakeTimeProcessor _brakeTimeProcessor;
    private readonly IBrakeTimeEvaluator _brakeTimeEvaluator;


    public AggregatorServiceBrakeTime(
    IBrakeTimeDataManager brakeTimeDataManager, IBrakeTimeProcessor brakeTimeProcessor, 
    IBrakeTimeEvaluator brakeTimeEvaluator)
    {
       

        _brakeTimeDataManager = brakeTimeDataManager;
        _brakeTimeProcessor = brakeTimeProcessor;
        _brakeTimeEvaluator = brakeTimeEvaluator;

    }


    public async Task<bool> AddOrUpdateBrakeTime(BrakeTimeDtoReqvest entity, bool IpStatus)
    {

        var brakeTimeResult = await _brakeTimeEvaluator.EvaluateBrakeTime(entity.UserId, IpStatus);

        bool BusyStatus = await _brakeTimeDataManager.GetBusyStatus(1);
        try
        {

            if (brakeTimeResult.BrakeTimeResult.StartTimeValidWorkSchedule && !brakeTimeResult.BrakeTimeResult.UserOfflineTimeDateDay && BusyStatus)
            {
                return await _brakeTimeProcessor.HandleValidWorkSchedule(brakeTimeResult.BrakeTimeResult, brakeTimeResult.ExistingBrake, entity.Id, IpStatus);
            }
            else if (brakeTimeResult.BrakeTimeResult.UserOnlineTimeDateDay && !brakeTimeResult.BrakeTimeResult.UserOfflineTimeDateDay && !BusyStatus && !IpStatus)
            {
                return await _brakeTimeProcessor.HandleOnlineTimeValid(brakeTimeResult.BrakeTimeResult, entity, IpStatus);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Database error occurred while saving changes.", ex);
        }

        return true;
    }


    //***************************************************************************************************************************//
}
