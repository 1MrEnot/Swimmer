namespace Swimmer.Domain.Entities;

public class AthleteOnSwim : BaseEntity
{
    public AthleteOnSwim(Athlete athlete, Swim swim, int row, TimeSpan? preliminaryTime)
    {
        Athlete = athlete;
        Swim = swim;
        Row = row;
        PreliminaryTime = preliminaryTime;
    }

    public Athlete Athlete { get; }

    public Swim Swim { get; }

    public int Row { get; }

    public TimeSpan? PreliminaryTime { get; }

    public TimeSpan? SwimTime { get; set; }

    public SwimState SwimState { get; set; }
}