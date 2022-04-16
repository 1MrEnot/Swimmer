namespace Swimmer.Domain.Entities;

using System.Diagnostics;
using ValueObjects;

public class Athlete : BaseEntity
{
    public Athlete(Name name, Year birthYear, Gender gender)
    {
        Name = name;
        BirthYear = birthYear;
        Gender = gender;
    }

    public Name Name { get; }

    public Year BirthYear { get; }

    public Gender Gender { get; }

    public override string ToString()
    {
        return $"{Name}: {BirthYear} ({Gender})";
    }
}