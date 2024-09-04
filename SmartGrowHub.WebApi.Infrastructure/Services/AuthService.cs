using Microsoft.EntityFrameworkCore;
using SmartGrowHub.Domain.Requests;
using SmartGrowHub.Domain.Responses;
using SmartGrowHub.WebApi.Application.Interfaces;
using SmartGrowHub.WebApi.Infrastructure.Data;
using SmartGrowHub.WebApi.Infrastructure.Data.Model.Extensions;

namespace SmartGrowHub.WebApi.Infrastructure.Services;

internal sealed class AuthService(ApplicationContext context, ITokenService tokenService) : IAuthService
{
    public TryOptionAsync<Fin<LogInResponse>> LogInAsync(LogInRequest request, CancellationToken cancellationToken) =>
        context.Users
            .Where(user => user.UserName == request.UserName.Value)
            .Where(user => user.Password == request.Password.Value)
            .FirstOrDefaultAsync(cancellationToken)
            .Map(Optional)
            .ToTryOptionAsync()
            .Map(user => user
                .TryToDomain()
                .Map(user => new LogInResponse(user,
                    tokenService.CreateToken(user))));

    public EitherAsync<Error, RegisterResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        context.Add(request.User.ToDb());
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
