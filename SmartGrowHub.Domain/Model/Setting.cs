using SmartGrowHub.Domain.Common;
using SmartGrowHub.Domain.Exceptions;
using System.Collections.Immutable;

namespace SmartGrowHub.Domain.Model;

public sealed record Setting(
    Id<Setting> Id,
    SettingType Type,
    SettingMode Mode,
    ImmutableArray<Component> Components)
{
    private static readonly ItemNotFoundException NotFoundException =
        new(nameof(Component), nameof(Setting));

    private static readonly ItemAlreadyExistsException AlreadyExistsException =
        new(nameof(Component), nameof(Setting));

    public static Identity<Setting> Create(
        Id<Setting> id, SettingType type, SettingMode mode,
        ImmutableArray<Component> components) =>
            Id<Setting>(new Setting(id, type, mode, components));

    public static Identity<Setting> Create(
        SettingType type, SettingMode mode,
        ImmutableArray<Component> components) =>
            Create(Common.Id.Create<Setting>(),
                type, mode, components);

    public override int GetHashCode() => Id.GetHashCode();

    public bool Equals(Setting? other) => other is not null && Id == other.Id;

    public Fin<Setting> AddComponent(Component component) =>
        !Components.Contains(component)
            ? this with { Components = Components.Add(component) }
            : FinFail<Setting>(AlreadyExistsException);

    public Fin<Setting> RemoveComponent(Component component)
    {
        int index = Components.IndexOf(component);
        if (index is -1) return FinFail<Setting>(NotFoundException);
        return this with { Components = Components.RemoveAt(index) };
    }
}