namespace Swimmer.Application;

using Domain.Entities;
using MediatR;
using Services;

public class GetAllCompetitionsQueryHandler : IRequestHandler<GetAllCompetitionsQuery, List<CompetitionDto>>
{
    private readonly IRepository<Competition> _repository;

    public GetAllCompetitionsQueryHandler(IRepository<Competition> repository)
    {
        _repository = repository;
    }

    public async Task<List<CompetitionDto>> Handle(GetAllCompetitionsQuery request, CancellationToken cancellationToken)
    {
        var competitions = await _repository.GetAll().ToListAsync();
        return competitions.Select(CompretitionMap.Map).ToList();
    }
}

public record GetAllCompetitionsQuery : IRequest<List<CompetitionDto>>;
