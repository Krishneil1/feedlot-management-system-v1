// -------------------------------------------------------------------------------------------------
//
// AnimalControllerTests.cs -- The AnimalControllerTests.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

using FeedlotApi.Application.Commands;
using FeedlotApi.Application.Queries;
using FeedlotApi.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace FeedlotApi.Controllers.Tests
{
    [TestClass]
    public class AnimalControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<ILogger<AnimalController>> _loggerMock;
        private AnimalController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<AnimalController>>();
            _controller = new AnimalController(_mediatorMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public async Task GetAnimalById_ReturnsOk_WhenAnimalExists()
        {
            var animal = new AnimalDto { TagId = "123", Breed = "Angus" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAnimalByIdQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(animal);

            var result = await _controller.GetAnimalById(1);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task AddAnimal_ReturnsOkWithId_WhenSuccessful()
        {
            // Arrange
            var command = new CreateAnimalCommand
            {
                AnimalDto = new AnimalDto
                {
                    TagId = "123",
                    Breed = "Angus",
                    DateOfBirth = DateTime.Today
                }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateAnimalCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _controller.AddAnimal(command);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsTrue(okResult.Value?.ToString()?.Contains("AnimalId") ?? false);
        }


        [TestMethod]
        public async Task UpdateAnimal_ReturnsNoContent_WhenSuccessful()
        {
            var dto = new AnimalDto { TagId = "123", Breed = "Angus", DateOfBirth = System.DateTime.Today };
            var result = await _controller.UpdateAnimal(1, dto);

            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteAnimal_ReturnsNoContent_WhenSuccessful()
        {
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteAnimalCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Unit.Value);

            var result = await _controller.DeleteAnimal(1);

            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
    }

}
