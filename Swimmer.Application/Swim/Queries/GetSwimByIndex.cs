namespace Swimmer.Application;

using Domain.Entities;
using MediatR;
using Services;

/// <summary>
/// Query, that returns swim of competition by its zero-based index
/// </summary>
/// <param name="CompetitionId">Competition id</param>
/// <param name="SwimIndex">Index of swim (starts from 0)</param>
public record GetSwimByIndexQuery(int CompetitionId, int SwimIndex) : IRequest<SwimDto>;

public class GetSwimByIndexQueryHandler : IRequestHandler<GetSwimByIndexQuery, SwimDto>
{
    private readonly IRepository<Competition> _repository;

    public GetSwimByIndexQueryHandler(IRepository<Competition> repository)
    {
        _repository = repository;
    }

    public async Task<SwimDto> Handle(GetSwimByIndexQuery request, CancellationToken cancellationToken)
    {
        var competition = await _repository.Get(request.CompetitionId);
        var nextSwim = competition.Swims[request.SwimIndex];
        return nextSwim.Map();
    }
}
