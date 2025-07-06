using Microsoft.EntityFrameworkCore;
using YourBonoPlatform.Shared.Infrastructure.Persistence.EFC.Configuration;
using YourBonoPlatform.Shared.Infrastructure.Persistence.EFC.Repositories;
using YourBonoPlatform.Bonds.Domain.Model.Entities;
using YourBonoPlatform.Bonds.Domain.Model.ValueObjects;
using YourBonoPlatform.Bonds.Domain.Repositories;

namespace YourBonoPlatform.Bonds.Infrastructure.Persistence.EFC.Repositories;

public class GracePeriodTypeRepository(AppDbContext context): BaseRepository<GracePeriodType>(context) 
    , IGracePeriodTypeRepository
{
    public async Task<bool> ExistsGracePeriodType(EGracePeriodTypes gracePeriodTypes)
    {
        return await Context.Set<GracePeriodType>()
            .AnyAsync(gracePeriodType => gracePeriodType.GracePeriod == gracePeriodTypes.ToString());
    }
}