// -------------------------------------------------------------------------------------------------
//
// AnimalController.cs -- The AnimalController.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Controllers;
using FeedlotApi.Application.Commands;
using FeedlotApi.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AnimalController : ControllerBase
{
    private readonly IMediator _mediator;

    public AnimalController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAnimals()
    {
        var result = await _mediator.Send(new GetAllAnimalsQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddAnimal([FromBody] AddAnimalCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { AnimalId = id });
    }
}
