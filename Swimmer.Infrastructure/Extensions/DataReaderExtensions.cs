namespace Swimmer.Infrastructure.Extensions;

using System.Data;

public static class DataReaderExtensions
{
    public static bool AnyDbNull(this IDataReader dataReader, params int[] indexes)
    {
        return indexes.Any(dataReader.IsDBNull);
    }
}