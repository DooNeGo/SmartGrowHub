namespace SmartGrowHub.Shared.Auth.Dto.LogIn;

public sealed record LoginResponseDto(
    Ulid UserId,
    string JwtToken);