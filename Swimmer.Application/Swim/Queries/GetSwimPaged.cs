namespace Swimmer.Application;

using Domain.Entities;
using MediatR;
using Services;

public class GetSwimPagedQueryHandler : IRequestHandler<GetSwimPagedQuery, List<SwimDto>>
{
    private readonly IRepository<Competition> _repository;

    public GetSwimPagedQueryHandler(IRepository<Competition> repository)
    {
        _repository = repository;
    }

    public async Task<List<SwimDto>> Handle(GetSwimPagedQuery request, CancellationToken cancellationToken)
    {
        var competition = await _repository.Get(request.CompetitionId);
        var requestedSwims = competition.Swims.Take(new Range(request.Shift, request.Count));
        return requestedSwims.Select(SwimMap.Map).ToList();
    }
}

public record GetSwimPagedQuery(int CompetitionId, int Shift, int Count) : IRequest<List<SwimDto>>;
