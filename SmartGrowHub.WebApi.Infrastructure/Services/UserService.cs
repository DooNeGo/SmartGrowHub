using SmartGrowHub.Domain.Common;
using SmartGrowHub.Domain.Model;
using SmartGrowHub.WebApi.Application.Interfaces;
using SmartGrowHub.WebApi.Infrastructure.Data;
using SmartGrowHub.WebApi.Infrastructure.Data.Model.Extensions;

namespace SmartGrowHub.WebApi.Infrastructure.Services;

internal sealed class UserService(ApplicationContext context) : IUserService
{
    public TryAsync<Unit> AddAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public TryAsync<Unit> DeleteAsync(Id<User> id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public TryOptionAsync<Fin<User>> GetAsync(Id<User> id, CancellationToken cancellationToken) =>
        context.Users
            .FindAsync([id.Value], cancellationToken)
            .AsTask()
            .Map(Optional)
            .ToTryOptionAsync()
            .Map(user => user.TryToDomain());
}
