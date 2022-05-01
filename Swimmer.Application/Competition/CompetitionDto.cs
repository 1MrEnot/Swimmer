namespace Swimmer.Application.Competition;

using Domain.Entities;

public class CompetitionDto
{
    public int Id { get; set; }

    public List<SwimDto> Swims { get; set; }
}

public static class CompretitionMap
{
    public static CompetitionDto Map(this Competition competition)
    {
        return new CompetitionDto
        {
            Id = competition.Id,
            Swims = competition.Swims.Select(SwimMap.Map).ToList()
        };
    }
}