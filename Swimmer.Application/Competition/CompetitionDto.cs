namespace Swimmer.Application;

using Domain.Entities;

public record CompetitionDto(int Id, string Name, int TrackCount, int SwimCount);

public static class CompretitionMap
{
    public static CompetitionDto Map(this Competition competition) =>
        new(competition.Id,
            competition.Name,
            competition.TrackCount,
            competition.Swims.Count);
}