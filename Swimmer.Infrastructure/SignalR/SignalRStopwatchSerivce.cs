namespace Swimmer.Infrastructure;

using Microsoft.AspNetCore.SignalR;
using Services;

public class SignalRStopwatchSerivce : IStopwatchSerivce
{
    private readonly IHubContext<StopwatchHub, IStopwatchClient> _hubContext;

    public SignalRStopwatchSerivce(IHubContext<StopwatchHub, IStopwatchClient> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task StartStopwatch(int swimId, DateTime startTime)
    {
        _hubContext.Clients.All.StopwatchStarted(swimId, startTime);
        return Task.CompletedTask;
    }

    public Task FinishAthleteSwim(int swimId, int athleteOnSwimId, TimeSpan time)
    {
        _hubContext.Clients.All.AthleteFinished(swimId, athleteOnSwimId, time);
        return Task.CompletedTask;
    }

    public Task EndSwim(int swimId)
    {
        _hubContext.Clients.All.SwimEnded(swimId);
        return Task.CompletedTask;
    }
}