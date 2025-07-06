using YourBonoPlatform.Shared.Infrastructure.Persistence.EFC.Configuration;
using YourBonoPlatform.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using YourBonoPlatform.Bonds.Domain.Model.Entities;
using YourBonoPlatform.Bonds.Domain.Repositories;

namespace YourBonoPlatform.Bonds.Infrastructure.Persistence.EFC.Repositories;

public class BondMetricsRepository(AppDbContext context): BaseRepository<BondMetrics>(context), IBondMetricsRepository
{
    public async Task<BondMetrics?> GetBondMetricsByBondId(int bondId)
    {
        return await Context.Set<BondMetrics>()
            .FirstOrDefaultAsync(b => b.BondId == bondId);
    } 
    public async Task<BondMetrics?> DeleteBondMetricsByBondId(int bondId)
    {
        var bondMetrics = await GetBondMetricsByBondId(bondId);
        if (bondMetrics != null)
        {
            Context.Set<BondMetrics>().Remove(bondMetrics);
            await Context.SaveChangesAsync();
        }
        return bondMetrics;
    }
}