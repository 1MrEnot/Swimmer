namespace Swimmer.Application;

using Domain.Entities;
using Domain.Enums;

public class SwimDto
{
    public int Id { get; set; }
    
    public int Index { get; set; }

    public Gender Gender { get; set; }

    public string DistanceName { get; set; } = string.Empty;

    public List<AthleteOnSwimDto> Athletes = new();
}

public static class SwimMap
{
    public static SwimDto Map(this Swim swim)
    {
        return new SwimDto
        {
            Id = swim.Id,
            Index = swim.Index,
            DistanceName = swim.DistanceName,
            Gender = swim.Gender,
            Athletes = swim.Athletes.Select(AthleteOnSwimMap.Map).ToList()
        };
    }
}