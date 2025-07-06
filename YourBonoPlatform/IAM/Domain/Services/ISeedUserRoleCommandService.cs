using YourBonoPlatform.IAM.Domain.Model.Commands;

namespace YourBonoPlatform.IAM.Domain.Services;

public interface ISeedUserRoleCommandService
{
    Task Handle(SeedUserRolesCommand command);
}