namespace FeedlotApi.Application.Handlers;

using AutoMapper;
using FeedlotApi.Application.Commands;
using FeedlotApi.Infrastructure.Interfaces;
using MediatR;

public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, Unit>
{
    private readonly IBookingService _service;
    private readonly IMapper _mapper;

    public UpdateBookingCommandHandler(IBookingService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
    {
        await _service.UpdateBookingAsync(request.Id, request.Booking, cancellationToken);
        return Unit.Value;
    }
}
