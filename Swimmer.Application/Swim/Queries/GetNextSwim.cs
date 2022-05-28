namespace Swimmer.Application;

using Domain.Entities;
using MediatR;
using Services;

public record GetNextSwimQuery(int CompetitionId) : IRequest<SwimDto?>;

public class GetNextSwimQueryHandler : IRequestHandler<GetNextSwimQuery, SwimDto?>
{
    private readonly IRepository<Competition> _repository;

    public GetNextSwimQueryHandler(IRepository<Competition> repository)
    {
        _repository = repository;
    }

    public async Task<SwimDto?> Handle(GetNextSwimQuery request, CancellationToken cancellationToken)
    {
        var competition = await _repository.Get(request.CompetitionId);
        var nextSwim = competition.Swims.FirstOrDefault(s => !s.Finished);
        return nextSwim?.Map();
    }
}
