namespace SmartGrowHub.Shared.Auth.Dto.LogIn;

public sealed record LogInRequestDto(
    string? UserName,
    string? Password);