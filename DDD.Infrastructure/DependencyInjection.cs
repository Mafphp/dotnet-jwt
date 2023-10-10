using DDD.Application.Common.Interfaces.Authentication;
using DDD.Application.Common.Interfaces.Services;
using DDD.Infrastructure.Authentication;
using DDD.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.Application;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }

}