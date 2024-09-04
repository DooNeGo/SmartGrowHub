using SmartGrowHub.Domain.Common;
using SmartGrowHub.Domain.Model;

namespace SmartGrowHub.WebApi.Application.Interfaces;

public interface IUserService
{
    TryAsync<Unit> AddAsync(User user, CancellationToken cancellationToken);
    TryOptionAsync<Fin<User>> GetAsync(Id<User> id, CancellationToken cancellationToken);
    TryAsync<Unit> DeleteAsync(Id<User> id, CancellationToken cancellationToken);
}