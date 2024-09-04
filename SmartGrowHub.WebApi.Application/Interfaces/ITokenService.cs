using SmartGrowHub.Domain.Model;

namespace SmartGrowHub.WebApi.Application.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}