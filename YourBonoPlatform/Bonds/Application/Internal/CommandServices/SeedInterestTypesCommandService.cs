using YourBonoPlatform.Bonds.Domain.Model.Commands;
using YourBonoPlatform.Bonds.Domain.Model.Entities;
using YourBonoPlatform.Bonds.Domain.Model.ValueObjects;
using YourBonoPlatform.Bonds.Domain.Repositories;
using YourBonoPlatform.Bonds.Domain.Services;
using YourBonoPlatform.Shared.Domain.Repositories;

namespace YourBonoPlatform.Bonds.Application.Internal.CommandServices;

public class SeedInterestTypesCommandService(IInterestTypeRepository repository, IUnitOfWork unitOfWork) : ISeedInterestTypesCommandService
{
    public async Task Handle(SeedInterestTypesCommand command)
    {
        foreach (EInterestTypes interestType in Enum.GetValues(typeof(EInterestTypes)))
        {
            if (await repository.ExistsInterestType(interestType)) continue;
            var userRole = new InterestType(interestType.ToString());
            await repository.AddAsync(userRole);
        }

        await unitOfWork.CompleteAsync();
    }
}