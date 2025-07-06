namespace YourBonoPlatform.Bonds.Domain.Model.Entities;

public class CashFlowItem
{
    
    public CashFlowItem(int id, int bondId, int period, DateTime paymentDate, bool isGracePeriod,
        decimal initialBalance, decimal interest, decimal amortization, decimal finalBalance, decimal totalPayment,
        decimal issuerCashFlow, decimal bondHolderCashFlow, decimal presentCashFlow,
        decimal presentCashFlowTimesPeriod, decimal convexityFactor)
    {
        Id = id;
        BondId = bondId;
        Period = period;
        PaymentDate = paymentDate;
        IsGracePeriod = isGracePeriod;
        InitialBalance = initialBalance;
        Interest = interest;
        Amortization = amortization;
        FinalBalance = finalBalance;
        TotalPayment = totalPayment;
        IssuerCashFlow = issuerCashFlow;
        BondHolderCashFlow = bondHolderCashFlow;
        PresentCashFlow = presentCashFlow;
        PresentCashFlowTimesPeriod = presentCashFlowTimesPeriod;
        ConvexityFactor = convexityFactor;
    }
    
    public int Id { get; private set; }
    public int BondId { get; private set; }
    public int Period { get; private set; }
    public DateTime PaymentDate { get; private set; }
    public bool IsGracePeriod { get; private set; }
    public decimal InitialBalance { get; private set; }
    public decimal Interest { get; private set; }
    public decimal Amortization { get; private set; }
    public decimal FinalBalance { get; private set; }
    public decimal TotalPayment { get; private set; }
    public decimal IssuerCashFlow { get; private set; }
    public decimal BondHolderCashFlow { get; private set; }
    public decimal PresentCashFlow { get; private set; }
    public decimal PresentCashFlowTimesPeriod { get; private set; }
    public decimal ConvexityFactor { get; private set; }

}