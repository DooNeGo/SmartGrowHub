using SmartGrowHub.Shared.Users.Dto;

namespace SmartGrowHub.Shared.Auth.Dto.LogIn;

public sealed record LoginResponseDto(
    UserDto User,
    string JwtToken);