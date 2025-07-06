namespace YourBonoPlatform.Bonds.Interfaces.REST.Resources;

public record CashFlowItemResource(
    int Id,
    int BondId,
    int Period,
    DateTime PaymentDate,
    bool IsGracePeriod,
    decimal InitialBalance,
    decimal Interest,
    decimal Amortization,
    decimal FinalBalance,
    decimal TotalPayment,
    decimal IssuerCashFlow,
    decimal BondHolderCashFlow,
    decimal PresentCashFlow,
    decimal PresentCashFlowTimesPeriod,
    decimal ConvexityFactor
    );