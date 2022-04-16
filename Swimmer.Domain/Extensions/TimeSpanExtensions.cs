namespace Swimmer.Domain;

public static class TimeSpanExtensions
{
    public static TimeSpan? SmartParse(this string? str)
    {
        if (str is not null && TimeSpan.TryParse(str, out var span))
            return span;

        return null;
    }
}