using System.Reflection;
using CarRental.Application.Common.Behaviors;
using CarRental.Application.Repositories;
using FluentValidation;
using CarRental.Persistence.Context;
using CarRental.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Persistence;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration  configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresSQL");
        services.AddDbContext<CarRentalDbContext>(opt => opt.UseNpgsql(connectionString));
        
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}