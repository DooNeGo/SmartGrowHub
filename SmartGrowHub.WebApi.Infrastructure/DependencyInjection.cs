using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartGrowHub.WebApi.Application.Interfaces;
using SmartGrowHub.WebApi.Infrastructure.Data;
using SmartGrowHub.WebApi.Infrastructure.Services;

namespace SmartGrowHub.WebApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
        services
            .AddDbContextPool<ApplicationContext>(options => { })
            .AddTransient<ITokenService, TokenService>()
            .AddTransient<IUserService, UserService>()
            .AddTransient<IAuthService, AuthService>();
}