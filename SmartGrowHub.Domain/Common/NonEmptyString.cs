using SmartGrowHub.Domain.Common.Interfaces;

namespace SmartGrowHub.Domain.Common;

public sealed record NonEmptyString : IValueObject<NonEmptyString, string>
{
    private const string ErrorMessage = "The value must not be empty or contain only spaces";

    private static readonly Error Error = Error.New(ErrorMessage);

    private NonEmptyString(string value) => Value = value;

    public string Value { get; }

    public static implicit operator string(NonEmptyString value) => value.Value;
    public static explicit operator NonEmptyString(string value) => Create(value).ThrowIfFail();

    public static Fin<NonEmptyString> Create(string rawValue) =>
        !string.IsNullOrWhiteSpace(rawValue) ? new NonEmptyString(rawValue)
            : FinFail<NonEmptyString>(Error);

    public override string ToString() => Value;
}