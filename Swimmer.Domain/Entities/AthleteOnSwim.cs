namespace Swimmer.Domain.Entities;

using Enums;

public class AthleteOnSwim : BaseEntity
{
    public AthleteOnSwim()
    {
        Athlete = null!;
    }
  
    public AthleteOnSwim(Athlete athlete, Swim swim, int row, TimeSpan? preliminaryTime)
    {
        Athlete = athlete;
        Row = row;
        PreliminaryTime = preliminaryTime;
    }

    public Athlete Athlete { get; }
    
    public int Row { get; }

    public TimeSpan? PreliminaryTime { get; }

    public TimeSpan? SwimTime { get; set; }

    public SwimState SwimState { get; set; }

    public override string ToString()
    {
        const string defaultTimeRepresentation = "NO_TIME";
        return $"{Row}) {Athlete.Name}: {PreliminaryTime?.ToString() ?? defaultTimeRepresentation}";
    }
}