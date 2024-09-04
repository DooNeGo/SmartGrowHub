namespace SmartGrowHub.WebApi.Application.Interfaces;

public interface IPasswordHasher
{
    string GetHash(string password);
    bool Verify(string password, string passwordHash);
}