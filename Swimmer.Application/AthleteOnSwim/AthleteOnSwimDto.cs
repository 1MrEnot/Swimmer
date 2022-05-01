namespace Swimmer.Application;

using Domain.Entities;
using Domain.Enums;

public class AthleteOnSwimDto
{
    public int Id { get; set; }
    
    public AthleteDto Athlete { get; set; } = null!;
    
    public int Row { get; set; }

    public TimeSpan? PreliminaryTime { get; set; }

    public TimeSpan? SwimTime { get; set; }

    public SwimState SwimState { get; set; }
}

public static class AthleteOnSwimMap
{
    public static AthleteOnSwimDto Map(this AthleteOnSwim athleteOnSwim)
    {
        return new AthleteOnSwimDto
        {
            Id = athleteOnSwim.Id,
            Row = athleteOnSwim.Row,
            Athlete = athleteOnSwim.Athlete.Map(),
            PreliminaryTime = athleteOnSwim.PreliminaryTime,
            SwimState = athleteOnSwim.SwimState,
            SwimTime = athleteOnSwim.SwimTime
        };
    }
}