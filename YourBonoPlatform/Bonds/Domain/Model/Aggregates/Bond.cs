using YourBonoPlatform.Bonds.Domain.Model.Commands;
using YourBonoPlatform.Bonds.Domain.Model.Entities;
using YourBonoPlatform.Bonds.Domain.Model.ValueObjects;

namespace YourBonoPlatform.Bonds.Domain.Model.Aggregates;

public class Bond
{
    public Bond()
    {
        UserId = 0;
        Name = string.Empty;
        NominalValue = 0.0m;
        MarketValue = 0.0m;
        Duration = 0;
        Frequency = 0;
        InterestRateTypeId = 0;
        InterestRate = 0.0m;
        Capitalization = 0;
        DiscountRate = 0.0m;
        EmissionDate = DateTime.MinValue;
        GracePeriodTypeId = 0;
        GracePeriodDuration = 0;
        CurrencyTypeId = 0;
        PremiumRate = 0.0m;
        StructuredRate = 0.0m;
        PlacementRate = 0.0m;
        FloatingRate = 0.0m;
        CavaliRate = 0.0m;
        DaysPerYear = 0;
        TaxRate = 0.0m;
    }
    public Bond(CreateBondCommand command)
    {
        UserId = command.UserId;
        Name = command.Name;
        NominalValue = command.NominalValue;
        MarketValue = command.MarketValue;
        Duration = command.Duration;
        Frequency = command.Frequency;
        InterestRateTypeId = command.InterestRateTypeId;
        InterestRate = command.InterestRate;
        Capitalization = command.Capitalization;
        DiscountRate = command.DiscountRate;
        EmissionDate = command.EmissionDate;
        GracePeriodTypeId = command.GracePeriodTypeId;
        GracePeriodDuration = command.GracePeriodDuration;
        CurrencyTypeId = command.CurrencyTypeId;
        PremiumRate = command.PremiumRate;
        StructuredRate = command.StructuredRate;
        PlacementRate = command.PlacementRate;
        FloatingRate = command.FloatingRate;
        CavaliRate = command.CavaliRate;
        DaysPerYear = command.DaysPerYear;
        TaxRate = command.TaxRate;
    }

    public void Update(UpdateBondCommand command)
    {
        Name = command.Name;
        NominalValue = command.NominalValue;
        MarketValue = command.MarketValue;
        Duration = command.Duration;
        Frequency = command.Frequency;
        InterestRateTypeId = command.InterestRateTypeId;
        InterestRate = command.InterestRate;
        Capitalization = command.Capitalization;
        DiscountRate = command.DiscountRate;
        EmissionDate = command.EmissionDate;
        GracePeriodTypeId = command.GracePeriodTypeId;
        GracePeriodDuration = command.GracePeriodDuration;
        CurrencyTypeId = command.CurrencyTypeId; 
        PremiumRate = command.PremiumRate;
        StructuredRate = command.StructuredRate;
        PlacementRate = command.PlacementRate;
        FloatingRate = command.FloatingRate;
        CavaliRate = command.CavaliRate;
        DaysPerYear = command.DaysPerYear;
        TaxRate = command.TaxRate;
    }
    
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public string Name { get; private set; }
    public decimal NominalValue { get; private set; }
    public decimal MarketValue { get; private set; }
    public int Duration { get; private set; }
    public int Frequency { get; private set; }
    public int InterestRateTypeId { get; private set; }
    public decimal InterestRate { get; private set; }
    public int Capitalization { get; private set; }
    public decimal DiscountRate { get; private set; }
    public DateTime EmissionDate { get; private set; }
    public int GracePeriodTypeId { get; private set; }
    public int GracePeriodDuration { get; private set; }
    public int CurrencyTypeId { get; private set; }
    public decimal PremiumRate { get; private set; }
    public decimal StructuredRate { get; private set; }
    public decimal PlacementRate { get; private set; }
    public decimal FloatingRate { get; private set; }
    public decimal CavaliRate { get; private set; }
    public int DaysPerYear { get; private set; }
    public decimal TaxRate { get; private set; }
    
}