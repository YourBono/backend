using YourBonoPlatform.Bonds.Domain.Model.Commands;
using YourBonoPlatform.Bonds.Domain.Model.Entities;
using YourBonoPlatform.Bonds.Domain.Model.ValueObjects;
using YourBonoPlatform.Bonds.Domain.Repositories;
using YourBonoPlatform.Bonds.Domain.Services;
using YourBonoPlatform.Shared.Domain.Repositories;

namespace YourBonoPlatform.Bonds.Application.Internal.CommandServices;

public class SeedCurrencyTypesCommandService(ICurrencyTypeRepository repository, IUnitOfWork unitOfWork) : ISeedCurrencyTypesCommandService
{
    public async Task Handle(SeedCurrencyTypesCommand command)
    {
        foreach (ECurrencyTypes currencyType in Enum.GetValues(typeof(ECurrencyTypes)))
        {
            if (await repository.ExistsCurrencyType(currencyType)) continue;
            var userRole = new CurrencyType(currencyType.ToString());
            await repository.AddAsync(userRole);
        }

        await unitOfWork.CompleteAsync();
    }
}