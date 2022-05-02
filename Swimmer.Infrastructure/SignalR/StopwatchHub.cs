namespace Swimmer.Infrastructure;

using Microsoft.AspNetCore.SignalR;
using Services;

public class StopwatchHub : Hub<IStopwatchClient>
{
    public const string HubPath = "/stopwatch";
}