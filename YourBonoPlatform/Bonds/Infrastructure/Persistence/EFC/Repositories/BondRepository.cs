using YourBonoPlatform.Shared.Infrastructure.Persistence.EFC.Configuration;
using YourBonoPlatform.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using YourBonoPlatform.Bonds.Domain.Model.Aggregates;
using YourBonoPlatform.Bonds.Domain.Repositories;

namespace YourBonoPlatform.Bonds.Infrastructure.Persistence.EFC.Repositories;

public class BondRepository(AppDbContext context): BaseRepository<Bond>(context), IBondRepository
{
    public async Task<IEnumerable<Bond>> GetAllBondsByUserId(int userId)
    {
        return await Context.Set<Bond>()
            .Where(b => b.UserId == userId)
            .ToListAsync();
    }
}