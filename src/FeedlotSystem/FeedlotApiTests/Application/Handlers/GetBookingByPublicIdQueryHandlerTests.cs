// -------------------------------------------------------------------------------------------------
//
// GetBookingByPublicIdQueryHandlerTests.cs -- The GetBookingByPublicIdQueryHandlerTests.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApiTests.Application.Handlers;
using System;
using System.Threading.Tasks;
using AutoMapper;
using FeedlotApi.Application.Handlers;
using FeedlotApi.Application.Queries;
using FeedlotApi.Domain.DTOs;
using FeedlotApi.Domain.Entities;
using FeedlotApi.Infrastructure.Interfaces;
using Moq;

[TestClass]
public class GetBookingByPublicIdQueryHandlerTests
{
    private Mock<IBookingService> _serviceMock = null!;
    private IMapper _mapper = null!;
    private GetBookingByPublicIdQueryHandler _handler = null!;

    [TestInitialize]
    public void Setup()
    {
        _serviceMock = new Mock<IBookingService>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Booking, BookingDto>();
        });

        _mapper = config.CreateMapper();
        _handler = new GetBookingByPublicIdQueryHandler(_serviceMock.Object, _mapper);
    }

    [TestMethod]
    public async Task Handle_ReturnsBookingDto_WhenBookingExists()
    {
        // Arrange
        var publicId = Guid.NewGuid();
        var booking = new Booking
        {
            Id = 1,
            PublicId = publicId,
            BookingNumber = "BK100",
            BookingDate = DateTime.Today,
            VendorName = "JBS",
            Property = "Dinmore",
            TruckReg = "ABC123",
            Status = "Pending"
        };

        _serviceMock.Setup(s => s.GetBookingByPublicIdAsync(publicId, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(booking);

        var request = new GetBookingByPublicIdQuery(publicId);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("BK100", result.BookingNumber);
        Assert.AreEqual(publicId, result.PublicId);
    }

    [TestMethod]
    public async Task Handle_ReturnsNull_WhenBookingDoesNotExist()
    {
        // Arrange
        var publicId = Guid.NewGuid();
        _serviceMock.Setup(s => s.GetBookingByPublicIdAsync(publicId, It.IsAny<CancellationToken>()))
                    .ReturnsAsync((Booking?)null);

        var request = new GetBookingByPublicIdQuery(publicId);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsNull(result);
    }
}
