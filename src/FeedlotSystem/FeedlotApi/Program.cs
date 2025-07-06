// -------------------------------------------------------------------------------------------------
// Program.cs -- The Program.cs class.
// -------------------------------------------------------------------------------------------------

using System.Reflection;
using System.Text.Json.Serialization;
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

// AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Add services
builder.Services.AddScoped<IAnimalService, AnimalService>();
builder.Services.AddScoped<IBookingService, BookingService>();

// Add controllers
builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        });
;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Add CORS to allow any origin (for MAUI app to call it)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// ✅ Apply migrations automatically
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FeedlotDbContext>();
    db.Database.Migrate();
}

// Swagger in development only
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ❌ Remove HTTPS redirection if not needed in local network
// app.UseHttpsRedirection(); // Commented out

app.UseCors("AllowAll"); // ✅ Enable CORS
app.UseAuthorization();

app.MapControllers();
app.Run();
