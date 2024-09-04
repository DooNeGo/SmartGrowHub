namespace SmartGrowHub.Domain.Common.Interfaces;

public interface IStronglyTyped<out TValue>
{
    public TValue Value { get; }
}