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

    public Task StartStopwatch(int competitionId, int swimId, DateTime startTime)
    {
        _hubContext.Clients.All.StopwatchStarted(competitionId, swimId, startTime);
        return Task.CompletedTask;
    }
}