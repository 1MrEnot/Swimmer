namespace Swimmer.Application;

using Domain.Entities;

public class CompetitionDto
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public int TrackCount { get; set; }

    public int SwimCount { get; set; }
}

public static class CompretitionMap
{
    public static CompetitionDto Map(this Competition competition)
    {
        return new CompetitionDto
        {
            Id = competition.Id,
            Name = competition.Name,
            TrackCount = competition.TrackCount,
            SwimCount = competition.Swims.Count
        };
    }
}