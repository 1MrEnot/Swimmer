namespace Swimmer.Application.Competition.Queries;

using Domain.Entities;
using MediatR;
using Services;

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
        NoEntityException<Competition>.ThrowIfNull(competition, request.Id);

        return competition.Map();
    }
}

public class GetCompetitionQuery : IRequest<CompetitionDto>
{
    public int Id { get; set; }
}