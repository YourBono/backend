using YourBonoPlatform.Bonds.Domain.Model.Commands;
using YourBonoPlatform.Bonds.Interfaces.REST.Resources;

namespace YourBonoPlatform.Bonds.Interfaces.REST.Transform;

public static class UpdateBondCommandFromResourceAssembler
{
    public static UpdateBondCommand ToCommandFromResource(int id,
        UpdateBondResource resource)
    {
        return new UpdateBondCommand(
            id,
            resource.Name,
            resource.NominalValue,
            resource.MarketValue,
            resource.Duration,
            resource.Frequency,
            resource.InterestRateTypeId,
            resource.InterestRate,
            resource.Capitalization,
            resource.DiscountRate,
            resource.EmissionDate,
            resource.GracePeriodTypeId,
            resource.GracePeriodDuration,
            resource.CurrencyTypeId,
            resource.PrimeRate,
            resource.StructuredRate,
            resource.PlacementRate,
            resource.FloatingRate,
            resource.CavaliRate
            
        );
    }
}