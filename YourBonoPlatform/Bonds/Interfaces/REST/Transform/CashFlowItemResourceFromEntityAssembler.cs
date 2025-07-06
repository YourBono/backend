using YourBonoPlatform.Bonds.Domain.Model.Entities;
using YourBonoPlatform.Bonds.Interfaces.REST.Resources;

namespace YourBonoPlatform.Bonds.Interfaces.REST.Transform;

public static class CashFlowItemResourceFromEntityAssembler
{
    public static CashFlowItemResource ToResourceFromEntity(CashFlowItem entity)
    {
        return new CashFlowItemResource(
            entity.Id,
            entity.BondId,
            entity.Period,
            entity.PaymentDate,
            entity.IsGracePeriod,
            entity.InitialBalance,
            entity.Interest,
            entity.Amortization,
            entity.FinalBalance,
            entity.TotalPayment,
            entity.IssuerCashFlow,
            entity.BondHolderCashFlow,
            entity.PresentCashFlow,
            entity.PresentCashFlowTimesPeriod,
            entity.ConvexityFactor
        );
    }
}