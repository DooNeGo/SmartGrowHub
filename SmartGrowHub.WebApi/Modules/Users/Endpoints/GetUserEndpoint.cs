using SmartGrowHub.Domain.Common;
using SmartGrowHub.Domain.Model;
using SmartGrowHub.Shared.Users.Extensions;
using SmartGrowHub.WebApi.Application.Interfaces;
using static Microsoft.AspNetCore.Http.Results;

namespace SmartGrowHub.WebApi.Modules.Users.Endpoints;

public static class GetUserEndpoint
{
    public static Task<IResult> GetUser(IUserService userService, Ulid ulid, CancellationToken cancellationToken) =>
        Id(new Id<User>(in ulid))
            .Map(id => userService.GetAsync(id, cancellationToken))
            .Map(task => task.Match(
                Some: fin => fin.Match(
                    Succ: user => Ok(user.ToDto()),
                    Fail: _ => StatusCode(500)),
                None: () => NotFound(),
                Fail: _ => StatusCode(500)))
            .Value;
}