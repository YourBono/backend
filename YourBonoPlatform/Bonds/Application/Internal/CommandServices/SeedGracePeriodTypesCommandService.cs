using YourBonoPlatform.Bonds.Domain.Model.Commands;
using YourBonoPlatform.Bonds.Domain.Model.Entities;
using YourBonoPlatform.Bonds.Domain.Model.ValueObjects;
using YourBonoPlatform.Bonds.Domain.Repositories;
using YourBonoPlatform.Bonds.Domain.Services;
using YourBonoPlatform.Shared.Domain.Repositories;

namespace YourBonoPlatform.Bonds.Application.Internal.CommandServices;

public class SeedGracePeriodTypesCommandService(IGracePeriodTypeRepository repository, IUnitOfWork unitOfWork) : ISeedGracePeriodTypesCommandService
{
    public async Task Handle(SeedGracePeriodTypesCommand command)
    {
        foreach (EGracePeriodTypes gracePeriodType in Enum.GetValues(typeof(EGracePeriodTypes)))
        {
            if (await repository.ExistsGracePeriodType(gracePeriodType)) continue;
            var userRole = new GracePeriodType(gracePeriodType.ToString());
            await repository.AddAsync(userRole);
        }

        await unitOfWork.CompleteAsync();
    }
}