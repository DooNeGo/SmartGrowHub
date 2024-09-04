using SmartGrowHub.Domain.Common;

namespace SmartGrowHub.Domain.Model;

public sealed record User(
    Id<User> Id,
    UserName UserName,
    Password Password,
    EmailAddress Email,
    NonEmptyString DisplayName)
{
    public static Fin<User> Create(
        Id<User> id, string userNameRaw, string passwordRaw,
        string emailRaw, string displayNameRaw) =>
            from userName in UserName.Create(userNameRaw)
            from password in Password.Create(passwordRaw)
            from email in EmailAddress.Create(emailRaw)
            from displayName in NonEmptyString.Create(displayNameRaw)
            select new User(id, userName, password, email, displayName);

    public static Fin<User> Create(
        string userNameRaw, string passwordRaw,
        string emailRaw, string displayNameRaw) =>
            Create(Common.Id.Create<User>(), userNameRaw,
                passwordRaw, emailRaw, displayNameRaw);

    public override int GetHashCode() => Id.GetHashCode();

    public bool Equals(User? other) => other is not null && Id == other.Id;
}