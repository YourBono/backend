using YourBonoPlatform.Shared.Domain.Repositories;
using YourBonoPlatform.Bonds.Domain.Model.Aggregates;

namespace YourBonoPlatform.Bonds.Domain.Repositories;

public interface IBondRepository: IBaseRepository<Bond>
{
    Task<IEnumerable<Bond>> GetAllBondsByUserId(int userId);
}