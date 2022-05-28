namespace Swimmer.Infrastructure;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Services;

public class SignalRStopwatchClient : BaseSignalRClient, IStopwatchClient
{
    public SignalRStopwatchClient(NavigationManager navigationManager) : base(navigationManager, StopwatchHub.HubPath)
    {
    }

    public void OnStopwatchStarted(Action<int, DateTime> action)
    {
        if (!Started)
            HubConnection.On(nameof(StopwatchStarted), action);
    }

    public void OnSwimEnded(Action<int> action)
    {
        if (!Started)
            HubConnection.On(nameof(SwimEnded), action);
    }

    public void OnAthleteFinished(Action<int, int, TimeSpan> action)
    {
        if (!Started)
            HubConnection.On(nameof(AthleteFinished), action);
    }
    
    public Task StopwatchStarted(int swimId, DateTime dateTime) => Task.CompletedTask;

    public Task AthleteFinished(int swimId, int athleteOnSwimId, TimeSpan time) => Task.CompletedTask;

    public Task SwimEnded(int swimId) => Task.CompletedTask;
}