using Booking.API.Middleware;
using Booking.Application;
using Booking.Application.Services;
using Booking.Domain.GeographicalData;
using Booking.Domain.Hotels;
using Booking.Infrastructure.Database.Repositories;
using Booking.Infrastructure.Pipelines;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(configuration =>
    configuration
        .RegisterServicesFromAssemblyContaining<AssemblyMarker>()
        .AddOpenBehavior(typeof(ValidationBehavior<,>)));

builder.Services.AddValidatorsFromAssemblyContaining<AssemblyMarker>();

builder.Services.AddTransient<ICurrentDateTimeService, CurrentDateTimeService>();

builder.Services
    .AddScoped<IGeographicalDataRepository, GeographicalDataRepository>()
    .AddScoped<IHotelRepository, HotelRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy =>
{
    policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

app.UseAuthorization();

app
    .UseMiddleware<ValidationExceptionMiddleware>()
    .UseMiddleware<DomainExceptionMiddleware>();

app.MapControllers();

app.Run();
