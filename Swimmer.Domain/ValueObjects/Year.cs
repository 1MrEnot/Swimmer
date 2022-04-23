namespace Swimmer.Domain.ValueObjects;

public record Year : IComparable<Year>
{
    public static readonly Year Empty = new();
    
    private const int MinumumYear = 1930;
    private static readonly int MaximumYear = DateTime.UtcNow.Year;

    public int YearValue { get; }

    private Year()
    {
        YearValue = MinumumYear;
    }
    
    private Year(int yearValue)
    {
        YearValue = yearValue;
    }

    public static Year FromInt(int? year)
    {
        Validate(year);
        return new Year(year!.Value);
    }

    public static implicit operator Year(int num) =>
        FromInt(num);

    private static void Validate(int? year)
    {
        if (year is null)
            throw new ArgumentNullException(nameof(year), "Year was null");

        if (year < MinumumYear)
            throw new ArgumentOutOfRangeException(nameof(year), $"Year must be greater than {MinumumYear}");

        if (year > MaximumYear)
            throw new ArgumentOutOfRangeException(nameof(year), $"Year must be less than {MaximumYear}");
    }

    public int CompareTo(Year? other)
    {
        return other is null 
            ? 1 
            : YearValue.CompareTo(other.YearValue);
    }

    public override string ToString()
    {
        return YearValue.ToString();
    }
}