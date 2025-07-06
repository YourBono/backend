using YourBonoPlatform.Bonds.Domain.Model.Commands;

namespace YourBonoPlatform.Bonds.Domain.Services;

public interface ISeedInterestTypesCommandService
{
    public Task Handle(SeedInterestTypesCommand command);
}