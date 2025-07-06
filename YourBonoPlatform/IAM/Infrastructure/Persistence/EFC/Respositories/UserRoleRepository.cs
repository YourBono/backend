using YourBonoPlatform.IAM.Domain.Model.Entities;
using YourBonoPlatform.IAM.Domain.Model.ValueObjects;
using YourBonoPlatform.IAM.Domain.Repositories;
using YourBonoPlatform.Shared.Infrastructure.Persistence.EFC.Configuration;
using YourBonoPlatform.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace YourBonoPlatform.IAM.Infrastructure.Persistence.EFC.Respositories;

public class UserRoleRepository(AppDbContext context) : BaseRepository<UserRole>(context), IUserRoleRepository
{
    public async Task<bool> ExistsUserRole(EUserRoles role)
    {
        return await Context.Set<UserRole>().AnyAsync(userRole => userRole.Role == role.ToString());
    }
}
