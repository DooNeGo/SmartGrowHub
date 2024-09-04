using SmartGrowHub.Shared.Auth.Dto.Register;
using SmartGrowHub.Shared.Auth.Extensions;
using SmartGrowHub.WebApi.Application.Interfaces;
using static Microsoft.AspNetCore.Http.Results;

namespace SmartGrowHub.WebApi.Modules.Auth.Endpoints;

public static class RegisterEndpoint
{
    public static Task<IResult> Register(IAuthService authService, RegisterRequestDto request, CancellationToken cancellationToken) =>
        request.TryToDomain()
            .Map(request => authService.RegisterAsync(request, cancellationToken))
            .Match(
                Succ: task => task.Match(
                    Left: error => BadRequest(error.Message),
                    Right: response => Ok(response)),
                Fail: error => BadRequest(error.Message).AsTask());
}