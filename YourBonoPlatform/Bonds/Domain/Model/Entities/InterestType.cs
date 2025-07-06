namespace YourBonoPlatform.Bonds.Domain.Model.Entities;

public class InterestType
{
    public int Id { get; set; }
    public string Interest { get; set; }
    
    public InterestType(string interest)
    {
        Interest = interest;
    }
}