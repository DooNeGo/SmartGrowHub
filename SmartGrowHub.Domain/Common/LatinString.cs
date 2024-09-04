using SmartGrowHub.Domain.Attributes;
using SmartGrowHub.Domain.Common.Interfaces;

namespace SmartGrowHub.Domain.Common;

public sealed record LatinString : IValueObject<LatinString, string>
{
    private const string ErrorMessage =
        "The value must consist only of Latin letters, digits and special characters";

    private static readonly LatinOnlyAttribute Attribute = new(ErrorMessage);

    private LatinString(string value) => Value = value;

    public static implicit operator string(LatinString value) => value.Value;
    public static explicit operator LatinString(string value) => Create(value).ThrowIfFail();

    public string Value { get; }

    public static Fin<LatinString> Create(string rawValue) =>
        Attribute.IsValid(rawValue).Map(v => new LatinString(v));

    public override string ToString() => Value;
}