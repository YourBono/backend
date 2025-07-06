using YourBonoPlatform.Shared.Domain.Repositories;
using YourBonoPlatform.Bonds.Domain.Model.Entities;

namespace YourBonoPlatform.Bonds.Domain.Repositories;

public interface ICashFlowItemRepository: IBaseRepository<CashFlowItem>
{
    Task<IEnumerable<CashFlowItem>> GetAllCashFlowItemsByBondId(int bondId);
    Task<IEnumerable<CashFlowItem>> DeleteAllCashFlowItemsByBondId(int bondId);
    Task<IEnumerable<CashFlowItem>> SaveAllCashFlowItems(IEnumerable<CashFlowItem> cashFlowItems);
}