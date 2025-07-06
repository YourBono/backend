using YourBonoPlatform.Bonds.Domain.Model.Aggregates;
using YourBonoPlatform.Bonds.Domain.Model.Queries;
using YourBonoPlatform.Bonds.Domain.Repositories;
using YourBonoPlatform.Bonds.Domain.Services;

namespace YourBonoPlatform.Bonds.Application.Internal.QueryServices;

public class BondQueryService(IBondRepository bondRepository): IBondQueryService
{
    public async Task<IEnumerable<Bond>> Handle(GetAllBondsByUserIdQuery query)
    {
        return await bondRepository.GetAllBondsByUserId(query.UserId);
    }

    public async Task<Bond?> Handle(GetBondByIdQuery query)
    {
        return await bondRepository.FindByIdAsync(query.Id);
    }
}