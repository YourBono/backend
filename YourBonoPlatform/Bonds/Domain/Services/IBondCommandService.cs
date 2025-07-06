using YourBonoPlatform.Bonds.Domain.Model.Aggregates;
using YourBonoPlatform.Bonds.Domain.Model.Commands;

namespace YourBonoPlatform.Bonds.Domain.Services;

public interface IBondCommandService
{
    Task<Bond?> Handle(CreateBondCommand command);
    Task<Bond?> Handle(UpdateBondCommand command);
    Task<Bond?> Handle(DeleteBondCommand command);
}