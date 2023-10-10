using DDD.Application.Common.Interfaces.Authentication;
using DDD.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.Application;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        return services;
    }

}