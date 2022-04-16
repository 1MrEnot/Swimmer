namespace Swimmer.Domain.ValueObjects;

public readonly record struct Name
{
    private readonly string _name;

    private Name(string name)
    {
        _name = name;
    }

    public static Name FromString(string? str)
    {
        Validate(str);
        return new Name(str!);
    }

    public static implicit operator Name(string str) =>
        FromString(str);

    private static void Validate(string? name)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name), "Name was null");

        if (name.Length < 2)
            throw new ArgumentOutOfRangeException(nameof(name), "Name length must be greater than one");
    }

    public override string ToString()
    {
        return _name ?? "NO_NAME";
    }
}