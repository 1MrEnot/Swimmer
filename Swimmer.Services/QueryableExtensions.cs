namespace Swimmer.Services;

public static class QueryableExtensions
{
    public static async Task<List<T>> ToListAsync<T>(this IQueryable<T> queryable)
    {
        if (queryable is not IAsyncEnumerable<T> asyncEnumerable)
        {
            return new List<T>(queryable);
        }

        var result = new List<T>();
        
        await foreach (var element in asyncEnumerable)
            result.Add(element);

        return result;
    }
}