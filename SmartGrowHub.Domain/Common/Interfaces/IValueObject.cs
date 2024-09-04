namespace SmartGrowHub.Domain.Common.Interfaces;

public interface IValueObject<TSelf, TValue>
    : IStronglyTyped<TValue>, ICreatable<Fin<TSelf>, TValue>
    where TSelf : IValueObject<TSelf, TValue>;