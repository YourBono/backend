using YourBonoPlatform.Bonds.Domain.Model.Entities;
using YourBonoPlatform.Bonds.Interfaces.REST.Resources;

namespace YourBonoPlatform.Bonds.Interfaces.REST.Transform;

public static class BondMetricsResourceFromEntityAssembler
{
    public static BondMetricsResource ToResourceFromEntity(BondMetrics entity)
    {
        return new BondMetricsResource(
            entity.Id,
            entity.BondId,
            entity.MaxPrice,
            entity.Duration,
            entity.Convexity,
            entity.ModifiedDuration,
            entity.Tcea,
            entity.Trea
        );
    }
}