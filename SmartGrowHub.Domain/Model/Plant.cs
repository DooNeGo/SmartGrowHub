using SmartGrowHub.Domain.Common;

namespace SmartGrowHub.Domain.Model;

public sealed record Plant(
    Id<Plant> Id, NonEmptyString Name)
{
    public static Fin<Plant> Create(string nameRaw) =>
        from name in NonEmptyString.Create(nameRaw)
        select new Plant(Common.Id.Create<Plant>(), name);

    public override int GetHashCode() => Id.GetHashCode();

    public bool Equals(Plant? other) => other is not null && Id == other.Id;
}