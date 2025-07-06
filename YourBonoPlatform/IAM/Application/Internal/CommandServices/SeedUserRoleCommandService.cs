using YourBonoPlatform.IAM.Domain.Model.Commands;
using YourBonoPlatform.IAM.Domain.Model.Entities;
using YourBonoPlatform.IAM.Domain.Model.ValueObjects;
using YourBonoPlatform.IAM.Domain.Repositories;
using YourBonoPlatform.IAM.Domain.Services;
using YourBonoPlatform.Shared.Domain.Repositories;

namespace YourBonoPlatform.IAM.Application.Internal.CommandServices;

public class SeedUserRoleCommandService(IUserRoleRepository repository, IUnitOfWork unitOfWork) : ISeedUserRoleCommandService
{
    public async Task Handle(SeedUserRolesCommand command)
    {
        foreach (EUserRoles role in Enum.GetValues(typeof(EUserRoles)))
        {
            if (await repository.ExistsUserRole(role)) continue;
            var userRole = new UserRole(role.ToString());
            await repository.AddAsync(userRole);
        }

        await unitOfWork.CompleteAsync();
    }
}