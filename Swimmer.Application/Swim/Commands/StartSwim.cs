namespace Swimmer.Application;

using Domain.Entities;
using Domain.Enums;
using MediatR;
using Services;

public class StartSwimCommandHandler : IRequestHandler<StartSwimCommand>
{
    private readonly IStopwatchSerivce _stopwatchSerivce;
    private readonly IRepository<Swim> _repository;

    public StartSwimCommandHandler(IStopwatchSerivce stopwatchSerivce, IRepository<Swim> repository)
    {
        _stopwatchSerivce = stopwatchSerivce;
        _repository = repository;
    }

    public async Task<Unit> Handle(StartSwimCommand request, CancellationToken cancellationToken)
    {
        var serverTime = DateTime.Now;

        var swim = await _repository.Get(request.SwimId);

        if (swim.StartTime is not null)
        {
            var mes = $"Swim {swim.Id} already started at {swim.StartTime}! Can not restart at {serverTime}";
            Console.WriteLine(mes);
            //throw new Exception(mes);
        }

        swim.StartTime = serverTime;
        var countingAthletes = swim.Athletes
            .Where(a => a.SwimState is SwimState.NotStarted or SwimState.DoesNotCount);
        foreach (var athleteOnSwim in countingAthletes)
            athleteOnSwim.SwimState = SwimState.Started;

        await _repository.SaveChanges();

        await _stopwatchSerivce.StartStopwatch(request.CompetitionId, swim.Id, serverTime);

        return Unit.Value;
    }
}

public record StartSwimCommand(int CompetitionId, int SwimId) : IRequest
{
    public DateTime DateTime { get; } = DateTime.Now;
}
