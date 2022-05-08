namespace Swimmer.Services;

public interface IStopwatchClient
{
    Task StopwatchStarted(int competitionId, int swimId, DateTime dateTime);

    Task SwimEnded(int competitionId, int swimId);
}
