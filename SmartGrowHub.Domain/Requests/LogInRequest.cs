using SmartGrowHub.Domain.Common;

namespace SmartGrowHub.Domain.Requests;

public sealed record LogInRequest(UserName UserName, Password Password);