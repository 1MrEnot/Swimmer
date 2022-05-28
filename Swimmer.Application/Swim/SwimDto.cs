namespace Swimmer.Application;

using Domain.Entities;
using Domain.Enums;

public record SwimDto(int Id, int Index, Gender Gender, string DistanceName, AthleteOnSwimDto[] Athletes);

public static class SwimMap
{
    public static SwimDto Map(this Swim swim) =>
        new(swim.Id,
            swim.Index,
            swim.Gender,
            swim.DistanceName,
            swim.Athletes.Select(AthleteOnSwimMap.Map).ToArray());
}