using SmartGrowHub.Domain.Model;
using SmartGrowHub.Shared.Users.Dto;

namespace SmartGrowHub.Shared.Users.Extensions;

public static class UserExtensions
{
    public static UserDto ToDto(this User user) =>
        new(user.Id, user.UserName, user.Email, user.DisplayName);
}
