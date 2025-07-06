using YourBonoPlatform.IAM.Domain.Model.Aggregates;
using YourBonoPlatform.Shared.Domain.Repositories;

namespace YourBonoPlatform.IAM.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByEmailAsync (string email);
    Task<bool> ExistsByUsername(string username);
    Task<string?> GetUsernameByIdAsync(int userId);
    bool ExistsById(int userId);
}