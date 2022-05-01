namespace Swimmer.Application;

using Domain.Entities;
using MediatR;
using Services;

public class GetSwimQueryHandler : IRequestHandler<GetSwimQuery, SwimDto>
{
    private readonly IRepository<Swim> _repository;

    public GetSwimQueryHandler(IRepository<Swim> repository)
    {
        _repository = repository;
    }

    public async Task<SwimDto> Handle(GetSwimQuery request, CancellationToken cancellationToken)
    {
        var swim = await _repository.Get(request.SwimId);
        NoEntityException<Swim>.ThrowIfNull(swim, request.SwimId);

        return swim.Map();
    }
}

public class GetSwimQuery : IRequest<SwimDto>
{
    public int SwimId { get; set; }
}