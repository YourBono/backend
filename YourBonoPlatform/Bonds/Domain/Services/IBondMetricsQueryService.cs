using YourBonoPlatform.Bonds.Domain.Model.Entities;
using YourBonoPlatform.Bonds.Domain.Model.Queries;

namespace YourBonoPlatform.Bonds.Domain.Services;

public interface IBondMetricsQueryService
{
    Task<BondMetrics?> Handle(GetBondMetricsByBondIdQuery query);
}