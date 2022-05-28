namespace Swimmer.Services;

public interface IStopwatchSerivce
{
    Task StartStopwatch(int swimId, DateTime startTime);

    Task FinishAthleteSwim(int swimId, int athleteOnSwimId, TimeSpan time);

    Task EndSwim(int swimId);
}