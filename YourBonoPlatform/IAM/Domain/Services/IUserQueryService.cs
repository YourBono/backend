using YourBonoPlatform.IAM.Domain.Model.Aggregates;
using YourBonoPlatform.IAM.Domain.Model.Queries;

namespace YourBonoPlatform.IAM.Domain.Services;

public interface IUserQueryService
{
    Task<User?> Handle(GetUserByIdQuery query);
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query);
    Task<User?> Handle(GetUserByEmailQuery query);
    
    Task<string?> Handle(GetUsernameByIdQuery query);
    bool Handle(UserExistsQuery query);
}