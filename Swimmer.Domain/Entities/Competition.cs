namespace Swimmer.Domain.Entities;

public class Competition
{
    private readonly Dictionary<int, Athlete> _athletes;
    private readonly List<Swim> _swims;

    public Competition()
    {
        _swims = new();
        _athletes = new();
    }

    public IReadOnlyList<Swim> Swims => _swims;

    public IReadOnlyDictionary<int, Athlete> Athletes => _athletes;

    public void AddAthlete(Athlete athlete)
    {
        if (_athletes.ContainsKey(athlete.Id))
            return;

        _athletes[athlete.Id] = athlete;
    }

    public void AddSwim(Gender gender, string distanceName)
    {
        var swim = new Swim(gender, distanceName, _swims.Count);
        _swims.Add(swim);
    }
}