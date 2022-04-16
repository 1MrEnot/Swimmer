namespace Swimmer.Domain.Entities;

using System.Diagnostics;

[DebuggerDisplay("{DistanceName}: {Gender}")]
public class Swim : BaseEntity
{
    private readonly SortedDictionary<int, AthleteOnSwim> _athletes;

    public Swim(Gender gender, string distanceName, int index)
    {
        _athletes = new();
        Gender = gender;
        DistanceName = distanceName;
        Index = index;
    }

    public int Index { get; }

    public Gender Gender { get; }

    public string DistanceName { get; }

    public IReadOnlyDictionary<int, AthleteOnSwim> Athletes => _athletes;

    public AthleteOnSwim AddAthleteForSwim(Athlete athlete, TimeSpan? preliminaryTime, int? row)
    {
        var actualRow = row ?? Athletes.Count;

        if (Athletes.ContainsKey(actualRow))
            throw new ArgumentOutOfRangeException(nameof(row),
                $"There is already {Athletes[actualRow]} on {actualRow} row");

        if (athlete.Gender != Gender)
            throw new ArgumentOutOfRangeException(nameof(athlete),
                $"{athlete} is not {Gender} and can not participate in the swim");

        var athleteOnSwim = new AthleteOnSwim(athlete, this, actualRow, preliminaryTime);
        _athletes[actualRow] = athleteOnSwim;

        return athleteOnSwim;
    }

    public override string ToString()
    {
        return $"{Index}) {DistanceName} ({Gender})";
    }
}