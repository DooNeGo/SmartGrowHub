using SmartGrowHub.Domain.Common.Interfaces;
using SmartGrowHub.Domain.Exceptions;

namespace SmartGrowHub.Domain.Common;

public readonly record struct CreatedAt : IValueObject<CreatedAt, DateOnly>
{
    private const string ErrorMessage =
        "The date must be less than or equal to today's date";

    private static readonly InvalidDateException Exception = new(ErrorMessage);
    private static readonly TimeProvider TimeProvider = TimeProvider.System;

    private CreatedAt(DateOnly date) => Value = date;

    public DateOnly Value { get; }

    public static implicit operator DateOnly(CreatedAt createdAt) => createdAt.Value;
    public static explicit operator CreatedAt(DateOnly date) => Create(date).ThrowIfFail();

    public static Fin<CreatedAt> Create(DateOnly rawValue) =>
        rawValue <= DateOnly.FromDateTime(TimeProvider.GetUtcNow().Date)
            ? new CreatedAt(rawValue) : FinFail<CreatedAt>(Exception);

    public override string ToString() => Value.ToString();
}