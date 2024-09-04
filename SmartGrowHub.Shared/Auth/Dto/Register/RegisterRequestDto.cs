namespace SmartGrowHub.Shared.Auth.Dto.Register;

public sealed record RegisterRequestDto(
    string? UserName,
    string? Password,
    string? Email,
    string? DisplayName);