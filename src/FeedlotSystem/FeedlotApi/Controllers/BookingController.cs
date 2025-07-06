// -------------------------------------------------------------------------------------------------
//
// BookingController.cs -- The BookingController.cs class.
//
// -------------------------------------------------------------------------------------------------

using FeedlotApi.Application.Commands;
using FeedlotApi.Application.Queries;
using FeedlotApi.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FeedlotApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<BookingController> _logger;

    public BookingController(IMediator mediator, ILogger<BookingController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetBookingById(int id)
    {
        try
        {
            var result = await _mediator.Send(new GetBookingByIdQuery(id));
            if (result == null)
            {
                _logger.LogWarning("Booking not found: {BookingId}", id);
                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting booking with ID {BookingId}", id);
            return StatusCode(500, "An error occurred while retrieving the booking.");
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllBookings()
    {
        try
        {
            var result = await _mediator.Send(new GetAllBookingsQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all bookings.");
            return StatusCode(500, "An error occurred while retrieving bookings.");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateBooking([FromBody] CreateBookingCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var id = await _mediator.Send(command);
            _logger.LogInformation("Created booking with ID {BookingId}", id);
            return Ok(new { BookingId = id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating booking.");
            return StatusCode(500, "An error occurred while creating the booking.");
        }
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateBooking(int id, [FromBody] UpdateBookingCommand command)
    {
        if (id != command.Id)
            return BadRequest("Booking ID in URL and body must match.");

        try
        {
            await _mediator.Send(command);
            _logger.LogInformation("Updated booking with ID {BookingId}", id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            _logger.LogWarning("Booking with ID {BookingId} not found.", id);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating booking with ID {BookingId}", id);
            return StatusCode(500, "An error occurred while updating the booking.");
        }
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteBooking(int id)
    {
        try
        {
            await _mediator.Send(new DeleteBookingCommand(id));
            _logger.LogInformation("Deleted booking with ID {BookingId}", id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            _logger.LogWarning("Booking with ID {BookingId} not found", id);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting booking with ID {BookingId}", id);
            return StatusCode(500, "An error occurred while deleting the booking.");
        }
    }
    [HttpGet("by-public-id/{publicId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByPublicId(Guid publicId)
    {
        try
        {
            var result = await _mediator.Send(new GetBookingByPublicIdQuery(publicId));
            if (result == null)
            {
                _logger.LogWarning("Booking not found for PublicId: {PublicId}", publicId);
                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving booking by PublicId: {PublicId}", publicId);
            return StatusCode(500, "An error occurred while retrieving the booking.");
        }
    }

    [HttpPut("{publicId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateByPublicId(Guid publicId, [FromBody] BookingDto dto)
    {
        try
        {
            await _mediator.Send(new UpdateBookingByPublicIdCommand { PublicId = publicId, Booking = dto });
            _logger.LogInformation("Updated booking with PublicId: {PublicId}", publicId);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            _logger.LogWarning("Booking with PublicId {PublicId} not found.", publicId);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating booking with PublicId: {PublicId}", publicId);
            return StatusCode(500, "An error occurred while updating the booking.");
        }
    }

}
