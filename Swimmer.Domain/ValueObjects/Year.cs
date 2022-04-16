namespace Swimmer.Domain.ValueObjects;

public readonly record struct Year : IComparable<Year>
{
    private const int MinumumYear = 1930;
    private static readonly int MaximumYear = DateTime.UtcNow.Year;

    private readonly int _year;

    private Year(int year)
    {
        _year = year;
    }

    public static Year FromInt(int? year)
    {
        Validate(year);
        return new Year(year!.Value);
    }

    private static void Validate(int? year)
    {
        if (year is null)
            throw new ArgumentNullException(nameof(year), "Year was null");

        if (year < MinumumYear)
            throw new ArgumentOutOfRangeException(nameof(year), $"Year must be greater than {MinumumYear}");

        if (year > MaximumYear)
            throw new ArgumentOutOfRangeException(nameof(year), $"Year must be less than {MaximumYear}");
    }

    public int CompareTo(Year other)
    {
        return _year.CompareTo(other._year);
    }

    public override string ToString()
    {
        return _year.ToString();
    }
}