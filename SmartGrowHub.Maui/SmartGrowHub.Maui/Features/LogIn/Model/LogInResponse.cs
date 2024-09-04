using SmartGrowHub.Maui.Users;

namespace SmartGrowHub.Maui.Features.LogIn.Model;

public sealed record LogInResponse(UserDto User, string JwtToken);