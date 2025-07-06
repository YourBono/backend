using YourBonoPlatform.Bonds.Domain.Model.Commands;

namespace YourBonoPlatform.Bonds.Domain.Services;

public interface ISeedGracePeriodTypesCommandService
{
    public Task Handle(SeedGracePeriodTypesCommand command);
}