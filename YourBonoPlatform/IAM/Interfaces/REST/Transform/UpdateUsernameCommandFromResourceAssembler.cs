using YourBonoPlatform.IAM.Domain.Model.Commands;
using YourBonoPlatform.IAM.Interfaces.REST.Resources;

namespace YourBonoPlatform.IAM.Interfaces.REST.Transform;

public static class UpdateUsernameCommandFromResourceAssembler
{
    public static UpdateUsernameCommand ToUpdateUsernameCommand(int id, UpdateUsernameResource resource)
    {
        return new UpdateUsernameCommand(id, resource.Username);
    }
}