using YourBonoPlatform.Bonds.Domain.Model.Aggregates;
using YourBonoPlatform.Bonds.Domain.Model.Entities;

namespace YourBonoPlatform.Bonds.Domain.Services;

public interface IBondValuationService
{
    Task<IEnumerable<CashFlowItem>> CalculateCashFlows(Bond bond);
    Task<BondMetrics?> CalculateBondMetrics(Bond bond, IEnumerable<CashFlowItem> cashFlowItems);
}