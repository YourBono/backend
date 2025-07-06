using YourBonoPlatform.Shared.Domain.Repositories;
using YourBonoPlatform.Bonds.Domain.Model.Entities;
using YourBonoPlatform.Bonds.Domain.Model.ValueObjects;

namespace YourBonoPlatform.Bonds.Domain.Repositories;

public interface ICurrencyTypeRepository: IBaseRepository<CurrencyType>
{
    public Task<bool> ExistsCurrencyType(ECurrencyTypes currencyTypes);
}