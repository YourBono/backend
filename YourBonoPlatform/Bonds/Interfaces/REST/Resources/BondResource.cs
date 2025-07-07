namespace YourBonoPlatform.Bonds.Interfaces.REST.Resources;

public record BondResource(
    int Id,
    int UserId,
    string Name,
    decimal NominalValue,
    decimal MarketValue,
    decimal Duration,
    int Frequency,
    int InterestRateTypeId,
    decimal InterestRate,
    int Capitalization,
    decimal DiscountRate,
    DateTime EmissionDate,
    int GracePeriodTypeId,
    int GracePeriodDuration,
    int CurrencyTypeId,
    decimal PremiumRate,
    decimal StructuredRate,
    decimal PlacementRate,
    decimal FloatingRate,
    decimal CavaliRate,
    int DaysPerYear,
    decimal TaxRate
    );