namespace Swimmer.Infrastructure;

using System.Data;

public static class DbDataRecordExtensions
{
    public static T Get<T>(this IDataRecord dataRecord, int i) where T: IConvertible
    {
        var value = dataRecord[i];
        if (value is T t)
            return t;

        var converted = Convert.ChangeType(value.ToString(), typeof(T));
        if (converted is T convT)
            return convT;

        throw new InvalidCastException($"Can not cast {value} to {typeof(T)}");
    }
}