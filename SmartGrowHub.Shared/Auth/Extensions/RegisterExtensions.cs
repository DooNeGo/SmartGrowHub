using SmartGrowHub.Domain.Model;
using SmartGrowHub.Domain.Requests;
using SmartGrowHub.Shared.Auth.Dto.Register;

namespace SmartGrowHub.Shared.Auth.Extensions;

public static class RegisterExtensions
{
    public static Fin<RegisterRequest> TryToDomain(this RegisterRequestDto request) =>
        User.Create(
            request.UserName ?? string.Empty,
            request.Password ?? string.Empty,
            request.Email ?? string.Empty,
            request.DisplayName ?? string.Empty)
        .Map(user => new RegisterRequest(user));
}
