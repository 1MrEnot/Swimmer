namespace Swimmer.Domain.Entities;

using Enums;
using ValueObjects;

public class Competition
{
    private readonly Dictionary<Name, Athlete> _athletes;
    private readonly List<Swim> _swims;

    public Competition()
    {
        _swims = new();
        _athletes = new();
    }

    public IReadOnlyList<Swim> Swims => _swims;

    public IReadOnlyDictionary<Name, Athlete> Athletes => _athletes;

    public void AddAthleteIfNotExist(Athlete athlete)
    {
        if (_athletes.ContainsKey(athlete.Name))
            return;

        _athletes[athlete.Name] = athlete;
    }

    public Swim CreateSwim(Gender gender, string distanceName, int? index=null)
    {
        var swim = new Swim(gender, distanceName, index ?? _swims.Count);
        _swims.Add(swim);
        return swim;
    }

    public bool RemoveSwim(Swim swim) =>
        _swims.Remove(swim);
}