using YourBonoPlatform.Bonds.Domain.Model.Aggregates;
using YourBonoPlatform.Bonds.Domain.Model.Queries;

namespace YourBonoPlatform.Bonds.Domain.Services;

public interface IBondQueryService
{
    Task<IEnumerable<Bond>> Handle(GetAllBondsByUserIdQuery query);
    Task<Bond?> Handle(GetBondByIdQuery query);
}