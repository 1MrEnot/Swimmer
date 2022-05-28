namespace Swimmer.Application.Commands;

using Domain.Entities;
using Domain.Enums;
using MediatR;
using Services;

public class StopAthleteOnSwimCommandHandler : IRequestHandler<StopAthleteOnSwimCommand>
{
    private readonly IRepository<Swim> _swimRepository;
    private readonly IStopwatchSerivce _stopwatchSerivce;

    public StopAthleteOnSwimCommandHandler(IRepository<Swim> swimRepository, IStopwatchSerivce stopwatchSerivce)
    {
        _swimRepository = swimRepository;
        _stopwatchSerivce = stopwatchSerivce;
    }

    public async Task<Unit> Handle(StopAthleteOnSwimCommand request, CancellationToken cancellationToken)
    {
        var swim = await _swimRepository.Get(request.SwimId);
        var athleteOnSwim = swim.Athletes.Single(a => a.Id == request.AthleteOnSwimId);

        if (athleteOnSwim.SwimState is SwimState.DoesNotCount or SwimState.Disqualification or SwimState.Started)
            athleteOnSwim.SwimTime = request.Time;
        else
        {
            throw new Exception(
                $"Athlete {athleteOnSwim.Athlete.Name} is {athleteOnSwim.SwimState}, but somehow finished swim on {athleteOnSwim.Track} track in {request.Time}");
        }
        
        if (swim.Athletes.All(a => a.SwimState is 
                SwimState.Absent or
                SwimState.Disqualification or
                SwimState.Done or
                SwimState.DoesNotCount))
        {
            await _stopwatchSerivce.EndSwim(swim.Id);
        }

        if (athleteOnSwim.SwimState == SwimState.Started)
            athleteOnSwim.SwimState = SwimState.Done;
        
        await _swimRepository.SaveChanges();
        await _stopwatchSerivce.FinishAthleteSwim(swim.Id, athleteOnSwim.Id, request.Time);

        return Unit.Value;
    }
}

public record StopAthleteOnSwimCommand(int SwimId, int AthleteOnSwimId, TimeSpan Time) : IRequest;
