namespace Swimmer.Application;

using System.Diagnostics.CodeAnalysis;

public class NoEntityException<T> : Exception
{
    private readonly int _id;

    private NoEntityException(int id)
    {
        _id = id;
    }

    public override string Message => $"No {typeof(T)} with {_id} id";

    public static void ThrowIfNull([NotNull] T? entity, int id)
    {
        if (entity is null) 
            throw new NoEntityException<T>(id);
    }
}