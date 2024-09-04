using SmartGrowHub.Domain.Common.Interfaces;
using SmartGrowHub.Domain.Exceptions;

namespace SmartGrowHub.Domain.Common;

public readonly record struct NonNegativeInteger : IValueObject<NonNegativeInteger, int>
{
    public static readonly NonNegativeInteger Zero = new(0);
    public static readonly NonNegativeInteger MaxValue = new(int.MaxValue);

    private const string ErrorMessage = "The value must not be negative";

    private static readonly InvalidIntegerException Exception = new(ErrorMessage);

    private NonNegativeInteger(int value) => Value = value;

    public int Value { get; }

    public static implicit operator int(NonNegativeInteger value) => value.Value;
    public static explicit operator NonNegativeInteger(int value) => Create(value).ThrowIfFail();

    public static Fin<NonNegativeInteger> Create(int rawValue) =>
        rawValue >= 0 ? new NonNegativeInteger(rawValue)
            : FinFail<NonNegativeInteger>(Exception);

    public override string ToString() => Value.ToString();
}
