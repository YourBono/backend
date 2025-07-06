using YourBonoPlatform.IAM.Domain.Model.Entities;
using YourBonoPlatform.IAM.Domain.Model.ValueObjects;
using YourBonoPlatform.Shared.Domain.Repositories;

namespace YourBonoPlatform.IAM.Domain.Repositories;

public interface IUserRoleRepository : IBaseRepository<UserRole>
{
    Task<bool> ExistsUserRole(EUserRoles role);
}