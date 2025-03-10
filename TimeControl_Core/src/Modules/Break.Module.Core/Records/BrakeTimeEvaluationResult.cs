public record BrakeTimeEvaluationResult
{
    public ResponseResultBrakeTime BrakeTimeResult { get; init; }
    public BrakeTime ExistingBrake { get; init; }

    public BrakeTimeEvaluationResult(ResponseResultBrakeTime brakeTimeResult, BrakeTime existingBrake)
    {
        BrakeTimeResult = brakeTimeResult;
        ExistingBrake = existingBrake;
    }
}