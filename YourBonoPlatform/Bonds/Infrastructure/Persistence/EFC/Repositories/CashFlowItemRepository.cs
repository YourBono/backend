using YourBonoPlatform.Shared.Infrastructure.Persistence.EFC.Configuration;
using YourBonoPlatform.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using YourBonoPlatform.Bonds.Domain.Model.Entities;
using YourBonoPlatform.Bonds.Domain.Repositories;

namespace YourBonoPlatform.Bonds.Infrastructure.Persistence.EFC.Repositories;

public class CashFlowItemRepository(AppDbContext context): BaseRepository<CashFlowItem>(context), ICashFlowItemRepository
{
    public async Task<IEnumerable<CashFlowItem>> GetAllCashFlowItemsByBondId(int bondId)
    {
        return await Context.Set<CashFlowItem>()
            .Where(c => c.BondId == bondId)
            .ToListAsync();
    }
    public async Task<IEnumerable<CashFlowItem>> DeleteAllCashFlowItemsByBondId(int bondId)
    {
        var cashFlowItems = await GetAllCashFlowItemsByBondId(bondId);
        var deleteAllCashFlowItemsByBondId = cashFlowItems.ToList();
        if (deleteAllCashFlowItemsByBondId.Any())
        {
            Context.Set<CashFlowItem>().RemoveRange(deleteAllCashFlowItemsByBondId);
            await Context.SaveChangesAsync();
        }
        return deleteAllCashFlowItemsByBondId;
    }
    //Save all 
    public async Task<IEnumerable<CashFlowItem>> SaveAllCashFlowItems(IEnumerable<CashFlowItem> cashFlowItems)
    {
        var saveAllCashFlowItems = cashFlowItems.ToList();
        Context.Set<CashFlowItem>().AddRange(saveAllCashFlowItems);
        await Context.SaveChangesAsync();
        return saveAllCashFlowItems;
    }
}