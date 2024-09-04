using SmartGrowHub.Domain.Common.Interfaces;
using SmartGrowHub.Domain.Extensions;

namespace SmartGrowHub.Domain.Common;

public sealed record Password : IValueObject<Password, string>
{
    private const int MinimumLength = 8;
    private const string Prefix = "Password:";

    private Password(string value) => Value = value;

    public string Value { get; }

    public static implicit operator string(Password value) => value.Value;
    public static explicit operator Password(string value) => Create(value).ThrowIfFail();

    public static Fin<Password> Create(string rawValue)
    {
        Fin<Password> result =
            from nonEmpty in NonEmptyString.Create(rawValue.Trim())
            from latin in LatinString.Create(nonEmpty)
            from minLength in NonNegativeInteger.Create(MinimumLength)
            from bounded in BoundedString.Create(latin, minLength, None)
            select new Password(bounded);

        return result.BiMap(
            x => x,
            error => error.AddPrefix(Prefix));
    }

    public override string ToString() => Value;
}
