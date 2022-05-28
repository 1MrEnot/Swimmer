namespace Swimmer.BlazorServer;

public static class TimeHelper
{
    private const string TimeFormat = @"m\.ss\.ff";

    public static string ToFormattedString(this TimeSpan? time) =>
        time switch
        {
            null => string.Empty,
            _ => time.Value.ToFormattedString()
        };

    public static string ToFormattedString(this TimeSpan time) =>
        time.ToString(TimeFormat);
}