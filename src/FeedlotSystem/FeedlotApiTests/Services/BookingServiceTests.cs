// -------------------------------------------------------------------------------------------------
//
// BookingServiceTests.cs -- The BookingServiceTests.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

using AutoMapper;
using FeedlotApi.Data;
using FeedlotApi.Domain.DTOs;
using FeedlotApi.Domain.Entities;
using FeedlotApi.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace FeedlotApiTests.Services
{
    [TestClass]
    public class BookingServiceTests
    {
        private FeedlotDbContext _context;
        private IMapper _mapper;
        private BookingService _service;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<FeedlotDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new FeedlotDbContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BookingDto, Booking>()
                   .ForMember(dest => dest.Id, opt => opt.Ignore())
                   .ForMember(dest => dest.Animals, opt => opt.Ignore());
                cfg.CreateMap<AnimalDto, Animal>()
                   .ForMember(dest => dest.Id, opt => opt.Ignore());
            });

            _mapper = config.CreateMapper();
            _service = new BookingService(_context, _mapper);
        }

        [TestMethod]
        public async Task CreateBookingAsync_SavesBooking()
        {
            var dto = new BookingDto
            {
                BookingNumber = "BK001",
                BookingDate = DateTime.Today,
                VendorName = "JBS",
                Property = "Dinmore",
                TruckReg = "ABC123",
                Status = "Pending",
                Animals = new List<AnimalDto>
                {
                    new AnimalDto { TagId = "001", Breed = "Angus", DateOfBirth = DateTime.Today }
                }
            };

            int id = await _service.CreateBookingAsync(dto, CancellationToken.None);
            var booking = await _context.Bookings.Include(b => b.Animals).FirstOrDefaultAsync(b => b.Id == id);

            Assert.IsNotNull(booking);
            Assert.AreEqual("BK001", booking.BookingNumber);
        }

        [TestMethod]
        public async Task GetBookingByIdAsync_ReturnsBooking()
        {
            var booking = new Booking { BookingNumber = "B123", BookingDate = DateTime.Today };
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            var result = await _service.GetBookingByIdAsync(booking.Id, CancellationToken.None);
            Assert.IsNotNull(result);
            Assert.AreEqual("B123", result.BookingNumber);
        }

        [TestMethod]
        public async Task DeleteBookingAsync_RemovesBooking()
        {
            var booking = new Booking { BookingNumber = "ToDelete", BookingDate = DateTime.Today };
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            await _service.DeleteBookingAsync(booking.Id, CancellationToken.None);

            var deleted = await _context.Bookings.FindAsync(booking.Id);
            Assert.IsNull(deleted);
        }
    }
}
