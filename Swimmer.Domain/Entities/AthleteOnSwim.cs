namespace Swimmer.Domain.Entities;

using Enums;

public class AthleteOnSwim : BaseEntity
{
    public AthleteOnSwim()
    {
        Athlete = null!;
    }
  
    public AthleteOnSwim(Athlete athlete, int track, TimeSpan? preliminaryTime)
    {
        Athlete = athlete;
        Track = track;
        PreliminaryTime = preliminaryTime;
    }

    public Athlete Athlete { get; }
    
    public int Track { get; }

    public TimeSpan? PreliminaryTime { get; }

    public TimeSpan? SwimTime { get; set; }

    public SwimState SwimState { get; set; }

    public override string ToString()
    {
        const string defaultTimeRepresentation = "NO_TIME";
        return $"{Track}) {Athlete.Name}: {PreliminaryTime?.ToString() ?? defaultTimeRepresentation}";
    }
}