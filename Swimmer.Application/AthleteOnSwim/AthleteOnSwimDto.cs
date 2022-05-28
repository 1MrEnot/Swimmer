namespace Swimmer.Application;

using Domain.Entities;
using Domain.Enums;

public record AthleteOnSwimDto(int Id, AthleteDto Athlete, int Track,
    TimeSpan? PreliminaryTime, TimeSpan? SwimTime, SwimState SwimState);

public static class AthleteOnSwimMap
{
    public static AthleteOnSwimDto Map(this AthleteOnSwim athleteOnSwim) =>
        new(athleteOnSwim.Id,
            athleteOnSwim.Athlete.Map(),
            athleteOnSwim.Track,
            athleteOnSwim.PreliminaryTime,
            athleteOnSwim.SwimTime,
            athleteOnSwim.SwimState);
}