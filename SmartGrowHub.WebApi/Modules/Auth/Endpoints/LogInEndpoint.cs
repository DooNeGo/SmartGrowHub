using SmartGrowHub.Shared.Auth.Dto.LogIn;
using SmartGrowHub.Shared.Auth.Extensions;
using SmartGrowHub.WebApi.Application.Interfaces;
using static Microsoft.AspNetCore.Http.Results;

namespace SmartGrowHub.WebApi.Modules.Auth.Endpoints;

public static class LogInEndpoint
{
    public static Task<IResult> LogIn(IAuthService authService, LogInRequestDto request, CancellationToken cancellationToken) =>
        request.TryToDomain()
            .Map(request => authService.LogInAsync(request, cancellationToken))
            .Match(
                Succ: task => task.Match(
                    Some: fin => fin.Match(
                        Succ: response => Ok(response.ToDto()),
                        Fail: e => BadRequest(e.Message)),
                    None: () => NotFound(),
                    Fail: e => BadRequest(e.Message)),
                Fail: error => BadRequest(error.Message).AsTask());
}