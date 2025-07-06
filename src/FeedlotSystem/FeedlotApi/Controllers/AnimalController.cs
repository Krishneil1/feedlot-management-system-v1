// -------------------------------------------------------------------------------------------------
//
// AnimalController.cs -- The AnimalController.cs class.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Controllers;

using FeedlotApi.Application.Commands;
using FeedlotApi.Application.Queries;
using FeedlotApi.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AnimalController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AnimalController> _logger;

    public AnimalController(IMediator mediator, ILogger<AnimalController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AnimalDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAnimalById(int id)
    {
        try
        {
            var result = await _mediator.Send(new GetAnimalByIdQuery(id));
            if (result == null)
            {
                _logger.LogWarning("Animal with ID {AnimalId} not found.", id);
                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving animal with ID {AnimalId}", id);
            return StatusCode(500, "An error occurred while retrieving the animal.");
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<AnimalDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAnimals()
    {
        try
        {
            var result = await _mediator.Send(new GetAllAnimalsQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all animals.");
            return StatusCode(500, "An error occurred while retrieving animals.");
        }
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAnimal(int id, [FromBody] AnimalDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _mediator.Send(new UpdateAnimalCommand { Id = id, Animal = dto });
            _logger.LogInformation("Updated animal with ID {AnimalId}", id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            _logger.LogWarning("Animal with ID {AnimalId} not found.", id);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating animal with ID {AnimalId}", id);
            return StatusCode(500, "An error occurred while updating the animal.");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddAnimal([FromBody] CreateAnimalCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var id = await _mediator.Send(command);
            _logger.LogInformation("Created animal with ID {AnimalId}", id);
            return Ok(new { AnimalId = id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating animal.");
            return StatusCode(500, "An error occurred while creating the animal.");
        }
    }
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAnimal(int id)
    {
        try
        {
            await _mediator.Send(new DeleteAnimalCommand(id));
            _logger.LogInformation("Deleted animal with ID {AnimalId}", id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            _logger.LogWarning("Animal with ID {AnimalId} not found", id);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting animal with ID {AnimalId}", id);
            return StatusCode(500, "An error occurred while deleting the animal.");
        }
    }

}
