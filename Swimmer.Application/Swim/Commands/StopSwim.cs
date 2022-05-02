namespace Swimmer.Application;

using Domain.Entities;
using Domain.Enums;
using MediatR;
using Services;

public class StopSwimCommandHandler : IRequestHandler<StopSwimCommand, TimeSpan>
{
    private readonly IRepository<AthleteOnSwim> _athleteRepository;
    private readonly IRepository<Swim> _swimRepository;

    public StopSwimCommandHandler(IRepository<Swim> swimRepository, IRepository<AthleteOnSwim> athleteRepository)
    {
        _swimRepository = swimRepository;
        _athleteRepository = athleteRepository;
    }

    public async Task<TimeSpan> Handle(StopSwimCommand request, CancellationToken cancellationToken)
    {
        var serverTime = DateTime.Now;
        var athleteOnSwim = await _athleteRepository.Get(request.AthleteOnSwimId);
        var swim = await _swimRepository.Get(request.SwimId);

        if (swim.StartTime is null)
            throw new Exception("Swim has no start time!");
        
        if (athleteOnSwim.SwimState is SwimState.Started)
            athleteOnSwim.SwimState = SwimState.Done;

        var elapsed = serverTime - swim.StartTime.Value;
        athleteOnSwim.SwimTime = elapsed;
        
        await _athleteRepository.SaveChanges();
        
        return elapsed;
    }
}

public record StopSwimCommand(int SwimId, int AthleteOnSwimId) : IRequest<TimeSpan>;
