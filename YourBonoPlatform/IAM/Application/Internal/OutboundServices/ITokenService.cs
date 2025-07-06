using YourBonoPlatform.IAM.Domain.Model.Aggregates;

namespace YourBonoPlatform.IAM.Application.Internal.OutboundServices;

public interface ITokenService
{
    string GenerateToken(User user);
}