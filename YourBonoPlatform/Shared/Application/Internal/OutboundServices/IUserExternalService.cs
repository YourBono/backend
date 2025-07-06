namespace YourBonoPlatform.Shared.Application.Internal.OutboundServices;

public interface IUserExternalService
{
    bool UserExists(int userId);
}