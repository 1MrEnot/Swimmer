namespace Swimmer.Services;

public class ConsoleStopwatchService : IStopwatchSerivce
{
    public Task StartStopwatch(int competitionId, int swimId, DateTime startTime)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"Swim {swimId} on {competitionId} competition stated at {startTime}");
        Console.ResetColor();

        return Task.CompletedTask;
    }
}