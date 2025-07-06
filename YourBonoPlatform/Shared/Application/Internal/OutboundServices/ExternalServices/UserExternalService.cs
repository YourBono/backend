using YourBonoPlatform.IAM.Interfaces.ACL;

namespace YourBonoPlatform.Shared.Application.Internal.OutboundServices.ExternalServices;

public class UserExternalService(IIamContextFacade iamContextFacade) : IUserExternalService
{
    public bool UserExists(int userId)
    {
        return iamContextFacade.UsersExists(userId);
    }
}