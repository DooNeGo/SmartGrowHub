namespace SmartGrowHub.Shared.Users.Dto;

public sealed record UserDto(
    Ulid Id,
    string UserName,
    string Email,
    string DisplayName);
