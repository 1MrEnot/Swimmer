namespace Swimmer.Domain.Entities;

using Enums;
using ValueObjects;

public class Athlete : BaseEntity
{
    private Athlete()
    {
        Name = Name.Empty;
        BirthYear = Year.Empty;
        Gender = Gender.Unknown;
    }
    
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