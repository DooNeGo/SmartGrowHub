using SmartGrowHub.Domain.Common;
using SmartGrowHub.Domain.Requests;
using SmartGrowHub.Domain.Responses;
using SmartGrowHub.Shared.Auth.Dto.LogIn;

namespace SmartGrowHub.Shared.Auth.Extensions;

public static class LogInExtensions
{
    public static Fin<LogInRequest> TryToDomain(this LogInRequestDto request) =>
        from userName in UserName.Create(request.UserName ?? string.Empty)
        from password in Password.Create(request.Password ?? string.Empty)
        select new LogInRequest(userName, password);

    public static LoginResponseDto ToDto(this LogInResponse response) =>
        new(response.Id.Value, response.JwtToken);
}