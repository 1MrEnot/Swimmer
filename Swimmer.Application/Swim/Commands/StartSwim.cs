namespace Swimmer.Application;

using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using Services;

public class StartSwimCommandHandler : IRequestHandler<StartSwimCommand>
{
    private readonly IStopwatchSerivce _stopwatchSerivce;
    private readonly IRepository<Swim> _repository;
    private readonly ILogger<StartSwimCommandHandler> _logger;

    public StartSwimCommandHandler(IStopwatchSerivce stopwatchSerivce, IRepository<Swim> repository,
        ILogger<StartSwimCommandHandler> logger)
    {
        _stopwatchSerivce = stopwatchSerivce;
        _repository = repository;
        _logger = logger;
    }

    public async Task<Unit> Handle(StartSwimCommand request, CancellationToken cancellationToken)
    {
        var serverTime = DateTime.Now;

        var swim = await _repository.Get(request.SwimId);

        if (swim.StartTime is not null)
        {
            _logger.LogWarning("Swim {SwimId} already started at {StartTime}! Can not restart at {NewTime}",
                swim.Id, swim.StartTime, serverTime);
        }

        swim.StartTime = serverTime;
        var countingAthletes = swim.Athletes
            .Where(a => a.SwimState is SwimState.NotStarted or SwimState.DoesNotCount);
        foreach (var athleteOnSwim in countingAthletes)
            athleteOnSwim.SwimState = SwimState.Started;

        await _repository.SaveChanges();

        await _stopwatchSerivce.StartStopwatch(request.CompetitionId, swim.Id, serverTime);

        _logger.LogInformation("Started swim {SwimId} at {StartTime}", swim.Id, swim.StartTime);

        return Unit.Value;
    }
}

public record StartSwimCommand(int CompetitionId, int SwimId, DateTime DateTime) : IRequest;