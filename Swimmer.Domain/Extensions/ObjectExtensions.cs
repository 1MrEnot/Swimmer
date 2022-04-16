namespace Swimmer.Domain;

using System.Text;

public static class ObjectExtensions
{
    public static string RecordToString(this object obj)
    {
        return obj.GetType()
            .GetProperties()
            .Select(info => (info.Name, Value: info.GetValue(obj, null) ?? "(null)"))
            .Aggregate(
                new StringBuilder(),
                (sb, pair) => sb.AppendLine($"{pair.Name}: {pair.Value}"),
                sb => sb.ToString());
    }
}