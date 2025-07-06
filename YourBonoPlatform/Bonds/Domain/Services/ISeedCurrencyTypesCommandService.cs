using YourBonoPlatform.Bonds.Domain.Model.Commands;

namespace YourBonoPlatform.Bonds.Domain.Services;

public interface ISeedCurrencyTypesCommandService
{
    public Task Handle(SeedCurrencyTypesCommand command);
}