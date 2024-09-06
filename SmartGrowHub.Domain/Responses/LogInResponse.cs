using SmartGrowHub.Domain.Common;
using SmartGrowHub.Domain.Model;

namespace SmartGrowHub.Domain.Responses;

public sealed record LogInResponse(Id<User> Id, string JwtToken);