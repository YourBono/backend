using YourBonoPlatform.Shared.Domain.Repositories;
using YourBonoPlatform.Bonds.Domain.Model.Entities;
using YourBonoPlatform.Bonds.Domain.Model.ValueObjects;

namespace YourBonoPlatform.Bonds.Domain.Repositories;

public interface IInterestTypeRepository: IBaseRepository<InterestType>
{
    public Task<bool> ExistsInterestType(EInterestTypes interestTypes);
}