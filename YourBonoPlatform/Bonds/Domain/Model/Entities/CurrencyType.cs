namespace YourBonoPlatform.Bonds.Domain.Model.Entities;

public class CurrencyType
{
    public int Id { get; set; }
    public string Currency { get; set; }
    
    public CurrencyType(string currency)
    {
        Currency = currency;
    }
}