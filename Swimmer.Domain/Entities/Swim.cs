namespace Swimmer.Domain.Entities;

using System.Diagnostics;
using Enums;

[DebuggerDisplay("{DistanceName}: {Gender}")]
public class Swim : BaseEntity
{
    private readonly ISet<AthleteOnSwim> _athletes;

    public Swim()
    {
        _athletes = new SortedSet<AthleteOnSwim>(new AthleteOnSwimComparer());
        Gender = Gender.Unknown;
        DistanceName = string.Empty;
        Index = 0;
    }
    
    public Swim(Gender gender, string distanceName, int index)
    {
        _athletes = new SortedSet<AthleteOnSwim>(new AthleteOnSwimComparer());
        Gender = gender;
        DistanceName = distanceName;
        Index = index;
    }

    public int Index { get; }

    public Gender Gender { get; }

    public string DistanceName { get; }
    
    public DateTime? StartTime { get; set; }

    public ICollection<AthleteOnSwim> Athletes => _athletes;

    public AthleteOnSwim AddAthleteForSwim(Athlete athlete, TimeSpan? preliminaryTime=null, int? row=null)
    {
        // Count from 1
        var actualRow = row ?? 
                        (Athletes.Any()
                            ? Athletes.Max(a => a.Track) + 1
                            : 1);

        if (athlete.Gender != Gender)
            throw new ArgumentOutOfRangeException(nameof(athlete),
                $"{athlete} is not {Gender} and can not participate in the swim");

        var athleteOnSwim = new AthleteOnSwim(athlete, actualRow, preliminaryTime);
        if (!_athletes.Add(athleteOnSwim))
            throw new ArgumentException("Failed to add athlete to swim", nameof(athlete));

        return athleteOnSwim;
    }

    public override string ToString()
    {
        return $"{Index}) {DistanceName} ({Gender})";
    }

    private class AthleteOnSwimComparer : IComparer<AthleteOnSwim>
    {
        public int Compare(AthleteOnSwim? x, AthleteOnSwim? y)
        {
            if (ReferenceEquals(x, y))
            {
                return 0;
            }

            if (ReferenceEquals(null, y))
            {
                return 1;
            }

            if (ReferenceEquals(null, x))
            {
                return -1;
            }

            return x.Track.CompareTo(y.Track);
        }
    }
}