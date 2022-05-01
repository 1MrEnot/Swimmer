namespace Swimmer.Domain.Entities;

using Enums;

public class Competition : BaseEntity
{
    private readonly HashSet<Athlete> _athletes;
    private readonly List<Swim> _swims;

    public Competition()
    {
        _swims = new();
        _athletes = new(new AthleteComparer());
    }

    public IReadOnlyList<Swim> Swims => _swims;

    public ICollection<Athlete> Athletes => _athletes;

    public void AddAthleteIfNotExist(Athlete athlete)
    {
        _athletes.Add(athlete);
    }

    public Swim CreateSwim(Gender gender, string distanceName, int? index=null)
    {
        var swim = new Swim(gender, distanceName, index ?? _swims.Count);
        _swims.Add(swim);
        return swim;
    }

    public bool RemoveSwim(Swim swim) =>
        _swims.Remove(swim);
    
    private class AthleteComparer : IEqualityComparer<Athlete>
    {
        public bool Equals(Athlete? x, Athlete? y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null))
            {
                return false;
            }

            if (ReferenceEquals(y, null))
            {
                return false;
            }

            if (x.GetType() != y.GetType())
            {
                return false;
            }

            return x.Name.Equals(y.Name) &&
                   x.BirthYear.Equals(y.BirthYear) &&
                   x.Gender == y.Gender;
        }

        public int GetHashCode(Athlete obj)
        {
            return HashCode.Combine(obj.Name, obj.BirthYear, (int)obj.Gender);
        }
    }
}