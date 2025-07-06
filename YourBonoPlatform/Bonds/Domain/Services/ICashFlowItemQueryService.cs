using YourBonoPlatform.Bonds.Domain.Model.Entities;
using YourBonoPlatform.Bonds.Domain.Model.Queries;

namespace YourBonoPlatform.Bonds.Domain.Services;

public interface ICashFlowItemQueryService
{
    Task<IEnumerable<CashFlowItem>> Handle(GetCashFlowByBondIdQuery query);
}