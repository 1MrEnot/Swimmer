namespace Swimmer.Application;

using MediatR;
using Domain.Entities;
using Services;

public record GetCompetitionQuery(int Id) : IRequest<CompetitionDto>;

public class GetCompetitionQueryHandler : IRequestHandler<GetCompetitionQuery, CompetitionDto>
{
    private readonly IRepository<Competition> _repository;

    public GetCompetitionQueryHandler(IRepository<Competition> repository)
    {
        _repository = repository;
    }

    public async Task<CompetitionDto> Handle(GetCompetitionQuery request, CancellationToken cancellationToken)
    {
        var competition = await _repository.Get(request.Id);
        return competition.Map();
    }
}
