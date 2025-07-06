// -------------------------------------------------------------------------------------------------
//
// UpdateAnimalCommandHandler.cs -- The UpdateAnimalCommandHandler.cs class.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Application.Handlers;

using AutoMapper;
using FeedlotApi.Application.Commands;
using FeedlotApi.Infrastructure.Interfaces;
using MediatR;

public class UpdateAnimalCommandHandler : IRequestHandler<UpdateAnimalCommand, Unit>
{
    private readonly IAnimalService _service;
    private readonly IMapper _mapper;

    public UpdateAnimalCommandHandler(IAnimalService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateAnimalCommand request, CancellationToken cancellationToken)
    {
        await _service.UpdateAnimalAsync(request.Id, request.Animal, cancellationToken);
        return Unit.Value;
    }
}
