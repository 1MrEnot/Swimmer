namespace Swimmer.Services;

public interface IStopwatchSerivce
{
    Task StartStopwatch(int competitionId, int swimId, DateTime startTime);
}