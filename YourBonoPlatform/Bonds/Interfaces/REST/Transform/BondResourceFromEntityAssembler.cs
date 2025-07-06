using YourBonoPlatform.Bonds.Domain.Model.Aggregates;
using YourBonoPlatform.Bonds.Interfaces.REST.Resources;

namespace YourBonoPlatform.Bonds.Interfaces.REST.Transform;

public static class BondResourceFromEntityAssembler
{
    public static BondResource ToResourceFromEntity(Bond entity)
    {
        return new BondResource(
            entity.Id,
            entity.UserId,
            entity.Name,
            entity.NominalValue,
            entity.MarketValue,
            entity.Duration,
            entity.Frequency,
            entity.InterestRateTypeId,
            entity.InterestRate,
            entity.Capitalization,
            entity.DiscountRate,
            entity.EmissionDate,
            entity.GracePeriodTypeId,
            entity.GracePeriodDuration,
            entity.CurrencyTypeId,
            entity.PrimeRate,
            entity.StructuredRate,
            entity.PlacementRate,
            entity.FloatingRate,
            entity.CavaliRate
        );
    }
}