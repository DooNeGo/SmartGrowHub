using SmartGrowHub.Domain.Model;
using SmartGrowHub.Domain.Requests;
using SmartGrowHub.Domain.Responses;

namespace SmartGrowHub.WebApi.Application.Interfaces;

public interface IAuthService
{
    TryOptionAsync<Fin<LogInResponse>> LogInAsync(LogInRequest request, CancellationToken cancellationToken);
    EitherAsync<Error, RegisterResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);
}