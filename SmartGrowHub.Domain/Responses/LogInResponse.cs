using SmartGrowHub.Domain.Model;

namespace SmartGrowHub.Domain.Responses;

public sealed record LogInResponse(User User, string JwtToken);