namespace YourBonoPlatform.Bonds.Domain.Model.Commands;

public record UpdateBondCommand(
    int Id,
    string Name,
    decimal NominalValue,
    decimal MarketValue,
    int Duration,
    int Frequency,
    int InterestRateTypeId,
    decimal InterestRate,
    int Capitalization,
    decimal DiscountRate,
    DateTime EmissionDate,
    int GracePeriodTypeId,
    int GracePeriodDuration,
    int CurrencyTypeId,
    decimal PrimeRate,
    decimal StructuredRate,
    decimal PlacementRate,
    decimal FloatingRate,
    decimal CavaliRate
    );