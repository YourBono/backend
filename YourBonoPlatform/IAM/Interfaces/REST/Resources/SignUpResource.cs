namespace YourBonoPlatform.IAM.Interfaces.REST.Resources;

public record SignUpResource(
    string Username,
    string Password,
    string Email
    );