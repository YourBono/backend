using YourBonoPlatform.IAM.Domain.Model.Commands;
using YourBonoPlatform.IAM.Interfaces.REST.Resources;

namespace YourBonoPlatform.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Email, resource.Password);
    }
}