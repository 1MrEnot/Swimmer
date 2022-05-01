namespace Swimmer.Application;

using Domain.Entities;
using Domain.Enums;

public class AthleteDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;

    public int BirthYear { get; set; }

    public Gender Gender { get; set; }
}

public static class AthleteMap
{
    public static AthleteDto Map(this Athlete athlete)
    {
        return new AthleteDto
        {
            Id = athlete.Id,
            Name = athlete.Name.NameValue,
            BirthYear = athlete.BirthYear.YearValue,
            Gender = athlete.Gender,
        };
    }
}