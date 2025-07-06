using YourBonoPlatform.Shared.Domain.Repositories;
using YourBonoPlatform.Bonds.Domain.Model.Entities;

namespace YourBonoPlatform.Bonds.Domain.Repositories;

public interface IBondMetricsRepository: IBaseRepository<BondMetrics>
{
    Task<BondMetrics?> GetBondMetricsByBondId(int bondId);
    Task<BondMetrics?> DeleteBondMetricsByBondId(int bondId);
}