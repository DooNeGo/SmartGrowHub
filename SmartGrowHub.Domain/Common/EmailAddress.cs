using SmartGrowHub.Domain.Common.Interfaces;
using SmartGrowHub.Domain.Exceptions;
using SmartGrowHub.Domain.Extensions;
using System.ComponentModel.DataAnnotations;

namespace SmartGrowHub.Domain.Common;

public sealed record EmailAddress : IValueObject<EmailAddress, string>
{
    private const string ErrorMessage = "Invalid email address";
    private const string Prefix = "Email:";

    private static readonly EmailAddressAttribute Attribute = new();
    private static readonly InvalidStringException Exception = new(ErrorMessage);

    private EmailAddress(string value) => Value = value;

    public string Value { get; }

    public static implicit operator string(EmailAddress email) => email.Value;
    public static explicit operator EmailAddress(string value) => Create(value).ThrowIfFail();

    public static Fin<EmailAddress> Create(string rawValue)
    {
        Fin<EmailAddress> result =
            from nonEmpty in NonEmptyString.Create(rawValue.Trim())
            from _ in IsValidEmailAddress(nonEmpty)
            from latin in LatinString.Create(nonEmpty)
            select new EmailAddress(latin);

        return result.BiMap(
            Succ: x => x,
            Fail: error => error.AddPrefix(Prefix));
    }

    private static Fin<string> IsValidEmailAddress(string value) =>
        Attribute.IsValid(value) ? value : FinFail<string>(Exception);

    public override string ToString() => Value;
}