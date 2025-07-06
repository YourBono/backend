using Microsoft.EntityFrameworkCore;
using YourBonoPlatform.Shared.Infrastructure.Persistence.EFC.Configuration;
using YourBonoPlatform.Shared.Infrastructure.Persistence.EFC.Repositories;
using YourBonoPlatform.Bonds.Domain.Model.Entities;
using YourBonoPlatform.Bonds.Domain.Model.ValueObjects;
using YourBonoPlatform.Bonds.Domain.Repositories;

namespace YourBonoPlatform.Bonds.Infrastructure.Persistence.EFC.Repositories;

public class InterestTypeRepository(AppDbContext context): BaseRepository<InterestType>(context), IInterestTypeRepository
{
    public async Task<bool> ExistsInterestType(EInterestTypes interestTypes)
    {
        return await Context.Set<InterestType>()
            .AnyAsync(interestType => interestType.Interest == interestTypes.ToString());
    }
}