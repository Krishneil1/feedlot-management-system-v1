// -------------------------------------------------------------------------------------------------
//
// AnimalController.cs -- The AnimalController.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Controllers;
using FeedlotApi.Application.Commands;

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

    [HttpPost]
    public async Task<IActionResult> AddAnimal([FromBody] AddAnimalCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { AnimalId = id });
    }
}
