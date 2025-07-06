using Microsoft.EntityFrameworkCore;
using YourBonoPlatform.Shared.Infrastructure.Persistence.EFC.Configuration;
using YourBonoPlatform.Shared.Infrastructure.Persistence.EFC.Repositories;
using YourBonoPlatform.Bonds.Domain.Model.Entities;
using YourBonoPlatform.Bonds.Domain.Model.ValueObjects;
using YourBonoPlatform.Bonds.Domain.Repositories;

namespace YourBonoPlatform.Bonds.Infrastructure.Persistence.EFC.Repositories;

public class CurrencyTypeRepository(AppDbContext context) : BaseRepository<CurrencyType>(context), ICurrencyTypeRepository
{
    public async Task<bool> ExistsCurrencyType(ECurrencyTypes currencyTypes)
    {
        return await Context.Set<CurrencyType>().AnyAsync(currencyType => currencyType.Currency == currencyTypes.ToString());
    }
}