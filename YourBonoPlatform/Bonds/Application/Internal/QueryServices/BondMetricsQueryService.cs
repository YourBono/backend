using YourBonoPlatform.Bonds.Domain.Model.Entities;
using YourBonoPlatform.Bonds.Domain.Model.Queries;
using YourBonoPlatform.Bonds.Domain.Repositories;
using YourBonoPlatform.Bonds.Domain.Services;

namespace YourBonoPlatform.Bonds.Application.Internal.QueryServices;

public class BondMetricsQueryService(IBondMetricsRepository bondMetricsRepository): IBondMetricsQueryService
{
    public async Task<BondMetrics?> Handle(GetBondMetricsByBondIdQuery query)
    {
        return await bondMetricsRepository.GetBondMetricsByBondId(query.BondId);
    }
}