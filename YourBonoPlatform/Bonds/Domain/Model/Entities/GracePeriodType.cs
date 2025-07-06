namespace YourBonoPlatform.Bonds.Domain.Model.Entities;

public class GracePeriodType
{
    public int Id { get; set; }
    public string GracePeriod { get; set; }
    
    public GracePeriodType(string gracePeriod)
    {
        GracePeriod = gracePeriod;
    }
}