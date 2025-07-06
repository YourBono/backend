namespace YourBonoPlatform.Bonds.Interfaces.REST.Resources;

public record UpdateBondResource(
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