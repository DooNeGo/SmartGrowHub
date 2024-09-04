using Microsoft.EntityFrameworkCore;
using SmartGrowHub.Domain.Requests;
using SmartGrowHub.Domain.Responses;
using SmartGrowHub.WebApi.Application.Interfaces;
using SmartGrowHub.WebApi.Infrastructure.Data;
using SmartGrowHub.WebApi.Infrastructure.Data.Model;
using SmartGrowHub.WebApi.Infrastructure.Data.Model.Extensions;

namespace SmartGrowHub.WebApi.Infrastructure.Services;

internal sealed class AuthService(
    ApplicationContext context,
    ITokenService tokenService,
    IPasswordHasher passwordHasher)
    : IAuthService
{
    public TryOptionAsync<Fin<LogInResponse>> LogInAsync(LogInRequest request, CancellationToken cancellationToken) =>
        context.Users
            .Where(user => user.UserName == request.UserName.Value)
            .SingleOrDefaultAsync(cancellationToken)
            .Map(Optional)
            .Map(option => option
                .Bind(user => passwordHasher.Verify(request.Password, user.Password)
                    ? Some(user) : None))
            .ToTryOptionAsync()
            .Map(user => user
                .TryToDomain()
                .Map(user => new LogInResponse(user,
                    tokenService.CreateToken(user))));

    public EitherAsync<Error, RegisterResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        UserDb userDb = request.User.ToDb();
        string hashedPassword = passwordHasher.GetHash(userDb.Password);
        context.Add(userDb with { Password = hashedPassword });

        return TryAsync(context.SaveChangesAsync(cancellationToken)
            .Map(_ => new RegisterResponse()))
            .ToEither()
            .MapLeft(error => error.Exception
                .Map(exception => exception is DbUpdateException
                    ? Error.New("User already exists")
                    : Error.New("Internal error"))
                .IfNone(Error.New("Internal error")));
    }
}
