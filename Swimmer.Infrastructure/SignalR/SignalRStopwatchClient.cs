namespace Swimmer.Infrastructure;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Services;

public class SignalRStopwatchClient : BaseSignalRClient, IStopwatchClient
{
    public SignalRStopwatchClient(NavigationManager navigationManager) : base(navigationManager, StopwatchHub.HubPath)
    {
    }

    public Task StopwatchStarted(int competitionId, int swimId, DateTime dateTime) =>
        Task.CompletedTask;

    public void OnStopwatchStarted(Action<int, int, DateTime> action)
    {
        if (!Started)
            HubConnection.On(nameof(StopwatchStarted), action);
    }
}