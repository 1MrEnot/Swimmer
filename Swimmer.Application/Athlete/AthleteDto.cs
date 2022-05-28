namespace Swimmer.Application;

using Domain.Entities;
using Domain.Enums;

public record AthleteDto(int Id, string Name, int BirthYear, Gender Gender);

public static class AthleteMap
{
    public static AthleteDto Map(this Athlete athlete) =>
        new(athlete.Id,
            athlete.Name.NameValue,
            athlete.BirthYear.YearValue,
            athlete.Gender);
}