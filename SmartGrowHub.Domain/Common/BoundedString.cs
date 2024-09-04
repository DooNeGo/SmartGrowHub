namespace SmartGrowHub.Domain.Common;

public sealed record BoundedString
{
    private BoundedString(string value) => Value = value;

    public string Value { get; }

    public static implicit operator string(BoundedString value) => value.Value;
    public static explicit operator BoundedString(string value) => Create(value).Value;

    public static Identity<BoundedString> Create(string rawValue) =>
        Id(new BoundedString(rawValue));

    public static Fin<BoundedString> Create(
        string rawValue, Option<NonNegativeInteger> minLength,
        Option<NonNegativeInteger> maxLength) =>
            from _1 in ValidateMinLength(rawValue, in minLength)
            from _2 in ValidateMaxLength(rawValue, in maxLength)
            select new BoundedString(rawValue);

    private static Fin<string> ValidateMaxLength(
        string value, in Option<NonNegativeInteger> maxLength) =>
            ValidateLength(value, in maxLength, GetMaxLengthError,
                (str, max) => str.Length <= max);

    private static Fin<string> ValidateMinLength(
        string value, in Option<NonNegativeInteger> minLength) =>
            ValidateLength(value, in minLength, GetMinLengthError,
                (str, min) => str.Length >= min || str.Length is 0);

    private static Fin<string> ValidateLength(
        string value,
        in Option<NonNegativeInteger> length,
        Func<int, Error> getError,
        Func<string, NonNegativeInteger, bool> isValid) =>
            length.Match(
                Some: length => isValid(value, length)
                    ? FinSucc(value)
                    : FinFail<string>(getError(length)),
                None: value);

    private static Error GetMinLengthError(int minLength) =>
        Error.New(GetMinLengthErrorMessage(minLength));

    private static Error GetMaxLengthError(int maxLength) =>
        Error.New(GetMaxLengthErrorMessage(maxLength));

    private static string GetMinLengthErrorMessage(int minLength) =>
        $"The value must not be shorter than {minLength}";

    private static string GetMaxLengthErrorMessage(int maxLength) =>
        $"The value must not be longer than {maxLength}";

    public override string ToString() => Value;
}