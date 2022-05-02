namespace Swimmer.Infrastructure;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

public abstract class BaseSignalRClient : IAsyncDisposable
{
    protected BaseSignalRClient(NavigationManager navigationManager, string hubPath)
    {
        HubConnection = new HubConnectionBuilder()
            .WithUrl(navigationManager.ToAbsoluteUri(hubPath))
            .WithAutomaticReconnect()
            .Build();
    }

    protected bool Started { get; private set; }
    
    protected HubConnection HubConnection { get; }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        await HubConnection.DisposeAsync();
    }
 
    public async Task Start()
    {
        if (!Started)
        {
            await HubConnection.StartAsync();
            Started = true;
        }
    }
}