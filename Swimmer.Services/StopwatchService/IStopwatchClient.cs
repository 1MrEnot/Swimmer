namespace Swimmer.Services;

public interface IStopwatchClient
{
    Task StopwatchStarted(int swimId, DateTime dateTime);

    Task AthleteFinished(int swimId, int athleteOnSwimId, TimeSpan time);
    
    Task SwimEnded(int swimId);
}
