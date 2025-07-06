// -------------------------------------------------------------------------------------------------
// Program.cs -- The Program.cs class.
// -------------------------------------------------------------------------------------------------

using System.Reflection;
using FeedlotApi.Data;
using FeedlotApi.Infrastructure.Interfaces;
using FeedlotApi.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// EF Core
builder.Services.AddDbContext<FeedlotDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Add services
builder.Services.AddScoped<IAnimalService, AnimalService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Auto-run migrations at startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FeedlotDbContext>();
    db.Database.Migrate();
}

// Swagger in dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
