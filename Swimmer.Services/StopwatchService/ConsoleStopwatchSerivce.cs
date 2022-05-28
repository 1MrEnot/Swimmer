namespace Swimmer.Services;

public class ConsoleStopwatchService : IStopwatchSerivce
{
    public Task StartStopwatch(int swimId, DateTime startTime) => Print($"Swim {swimId} stated at {startTime}");

    public Task FinishAthleteSwim(int swimId, int athleteOnSwimId, TimeSpan time) =>
        Print($"Athlete on swim {athleteOnSwimId} finished swim {swimId} in {time}");

    public Task EndSwim(int swimId) => Print($"Swim {swimId} ended");

    private static Task Print(string text)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(text);
        Console.ResetColor();
        
        return Task.CompletedTask;
    }

}