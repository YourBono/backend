using YourBonoPlatform.Bonds.Domain.Model.Entities;
using YourBonoPlatform.Bonds.Domain.Model.Queries;
using YourBonoPlatform.Bonds.Domain.Repositories;
using YourBonoPlatform.Bonds.Domain.Services;

namespace YourBonoPlatform.Bonds.Application.Internal.QueryServices;

public class CashFlowItemQueryService(ICashFlowItemRepository cashFlowItemRepository) : ICashFlowItemQueryService
{
    public async Task<IEnumerable<CashFlowItem>> Handle(GetCashFlowByBondIdQuery query)
    {
        return await cashFlowItemRepository.GetAllCashFlowItemsByBondId(query.BondId);
    }
}