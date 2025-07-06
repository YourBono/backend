using YourBonoPlatform.Bonds.Domain.Model.Commands;
using YourBonoPlatform.Bonds.Interfaces.REST.Resources;

namespace YourBonoPlatform.Bonds.Interfaces.REST.Transform;

public static class CreateBondCommandFromResourceAssembler
{
    public static CreateBondCommand ToCommandFromResource(CreateBondResource resource)
    {
        return new CreateBondCommand(
            resource.UserId,
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