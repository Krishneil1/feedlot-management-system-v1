// -------------------------------------------------------------------------------------------------
//
// BookingControllerTests.cs -- The BookingControllerTests.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

using FeedlotApi.Application.Commands;
using FeedlotApi.Application.Queries;
using FeedlotApi.Controllers;
using FeedlotApi.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedlotApiTests.Controllers
{
    [TestClass]
    public class BookingControllerTests
    {
        private Mock<IMediator> _mediatorMock = null!;
        private Mock<ILogger<BookingController>> _loggerMock = null!;
        private BookingController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<BookingController>>();
            _controller = new BookingController(_mediatorMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public async Task GetBookingById_ReturnsOk_WhenFound()
        {
            // Arrange
            int bookingId = 1;
            var booking = new BookingDto {  BookingNumber = "BK001" };

            _mediatorMock.Setup(m => m.Send(It.Is<GetBookingByIdQuery>(q => q.Id == bookingId), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(booking);

            // Act
            var result = await _controller.GetBookingById(bookingId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetBookingById_ReturnsNotFound_WhenNull()
        {
            // Arrange
            int bookingId = 99;

            _mediatorMock.Setup(m => m.Send(It.Is<GetBookingByIdQuery>(q => q.Id == bookingId), It.IsAny<CancellationToken>()))
                         .ReturnsAsync((BookingDto?)null);

            // Act
            var result = await _controller.GetBookingById(bookingId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetAllBookings_ReturnsOk_WithList()
        {
            var bookings = new List<BookingDto> { new BookingDto { BookingNumber = "BK001" } };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllBookingsQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(bookings);

            var result = await _controller.GetAllBookings();

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task CreateBooking_ReturnsOk_WhenSuccessful()
        {
            var command = new CreateBookingCommand
            {
                Booking = new BookingDto { BookingNumber = "BK001" }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateBookingCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(10);

            var result = await _controller.CreateBooking(command);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetByPublicId_ReturnsOk_WhenFound()
        {
            var publicId = Guid.NewGuid();
            var dto = new BookingDto { BookingNumber = "BKXYZ" };

            _mediatorMock.Setup(m => m.Send(It.Is<GetBookingByPublicIdQuery>(q => q.PublicId == publicId), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(dto);

            var result = await _controller.GetByPublicId(publicId);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
    }
}
