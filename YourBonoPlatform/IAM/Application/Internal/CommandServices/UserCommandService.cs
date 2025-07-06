using YourBonoPlatform.IAM.Application.Internal.OutboundServices;
using YourBonoPlatform.IAM.Domain.Model.Aggregates;
using YourBonoPlatform.IAM.Domain.Model.Commands;
using YourBonoPlatform.IAM.Domain.Repositories;
using YourBonoPlatform.IAM.Domain.Services;
using YourBonoPlatform.Shared.Application.Internal.OutboundServices;
using YourBonoPlatform.Shared.Domain.Repositories;

namespace YourBonoPlatform.IAM.Application.Internal.CommandServices;

public class UserCommandService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork)
    : IUserCommandService
{
    public async Task<(User user, string token)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindByEmailAsync(command.Email);

        if (user == null || !hashingService.VerifyPassword(command.Password, user.PasswordHash) ||
            !command.Email.Contains('@'))
            throw new Exception("Invalid email or password");

        var token = tokenService.GenerateToken(user);

        return (user, token);
    }

    public async Task<User?> Handle(SignUpCommand command)
    {
        const string symbols = "!@#$%^&*()_-+=[{]};:>|./?";
        if (command.Password.Length < 8 || !command.Password.Any(char.IsDigit) || !command.Password.Any(char.IsUpper) ||
            !command.Password.Any(char.IsLower) || !command.Password.Any(c => symbols.Contains(c)))
            throw new Exception(
                "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit and one special character");
        
        if(!command.Email.Contains('@'))
            throw new Exception("Invalid email address");
        
        if (command.Phone.Length < 9)
            throw new Exception("Phone number must to be valid");

        if (await userRepository.ExistsByUsername(command.Username))
            throw new Exception($"Username {command.Username} is already taken");

        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command.Username, hashedPassword, command.Email);
        
        await userRepository.AddAsync(user);
        await unitOfWork.CompleteAsync();

        return user;
    }

    public async Task<User?> Handle(UpdateUsernameCommand command)
    {
        var userToUpdate = await userRepository.FindByIdAsync(command.Id);
        if (userToUpdate == null)
            throw new Exception("User not found");
        var userExists = await userRepository.ExistsByUsername(command.Username);
        if (userExists)
        {
            throw new Exception("This username already exists");
        }

        userToUpdate.UpdateUsername(command.Username);
        await unitOfWork.CompleteAsync();
        return userToUpdate;
    }
    
}

