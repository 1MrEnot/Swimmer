﻿namespace Swimmer.Application;

using Domain.Entities;
using MediatR;
using Services;

public record GetSwimsOnTrackQuery(int CompetitionId, int TrackNumber) : IRequest<List<SwimDto>>;

public class GetSwimsOnTrackQueryHandler : IRequestHandler<GetSwimsOnTrackQuery, List<SwimDto>>
{
    private readonly IRepository<Competition> _repository;

    public GetSwimsOnTrackQueryHandler(IRepository<Competition> repository)
    {
        _repository = repository;
    }

    public async Task<List<SwimDto>> Handle(GetSwimsOnTrackQuery request, CancellationToken cancellationToken)
    {
        var competition = await _repository.Get(request.CompetitionId);
        var swimsOnTrack = competition.Swims
            .Select(s => GetSwimsOnTrack(s, request.TrackNumber))
            .ToList();
        
        return swimsOnTrack;
    }

    private static SwimDto GetSwimsOnTrack(Swim s, int trackNumber) =>
        new(
            s.Id,
            s.Index,
            s.Gender,
            s.DistanceName,
            s.Athletes
                .Where(a => a.Track == trackNumber)
                .Select(AthleteOnSwimMap.Map)
                .ToArray());
}
