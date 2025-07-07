namespace YourBonoPlatform.Bonds.Interfaces.REST.Resources;

public record BondMetricsResource(
    int Id,
    int BondId,
    decimal MaxPrice,
    decimal Duration,
    decimal Convexity,
    decimal ModifiedDuration,
    decimal Tcea,
    decimal Trea,
    decimal NetPresentValue,
    decimal Cok,
    decimal TceaWithShield
    );