using YourBonoPlatform.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using YourBonoPlatform.IAM.Domain.Model.Aggregates;
using YourBonoPlatform.IAM.Domain.Model.Entities;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using YourBonoPlatform.Bonds.Domain.Model.Aggregates;
using YourBonoPlatform.Bonds.Domain.Model.Entities;

namespace YourBonoPlatform.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        // Enable Audit Fields Interceptors
        builder.AddCreatedUpdatedInterceptor();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        //IAM Context
        
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Username).IsRequired();
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
        builder.Entity<User>().Property(u => u.Email).IsRequired();
        builder.Entity<UserRole>().HasMany<User>().WithOne().HasForeignKey(u => u.RoleId);

        builder.Entity<UserRole>().HasKey(ur => ur.Id);
        builder.Entity<UserRole>().Property(ur => ur.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<UserRole>().Property(ur => ur.Role).IsRequired();
        
        //Bonds Context
        builder.Entity<Bond>().HasKey(b => b.Id);
        builder.Entity<Bond>().Property(b => b.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Bond>().Property(b => b.UserId).IsRequired();
        builder.Entity<Bond>().Property(b => b.Name).IsRequired();
        builder.Entity<Bond>().Property(b => b.NominalValue).IsRequired();
        builder.Entity<Bond>().Property(b => b.MarketValue).IsRequired();
        builder.Entity<Bond>().Property(b => b.Duration).IsRequired();
        builder.Entity<Bond>().Property(b => b.Frequency).IsRequired();
        builder.Entity<Bond>().Property(b => b.InterestRateTypeId).IsRequired();
        builder.Entity<Bond>().Property(b => b.InterestRate).IsRequired();
        builder.Entity<Bond>().Property(b => b.Capitalization).IsRequired();
        builder.Entity<Bond>().Property(b => b.DiscountRate).IsRequired();
        builder.Entity<Bond>().Property(b => b.EmissionDate).IsRequired();
        builder.Entity<Bond>().Property(b => b.GracePeriodTypeId).IsRequired();
        builder.Entity<Bond>().Property(b => b.GracePeriodDuration).IsRequired();
        builder.Entity<Bond>().Property(b => b.CurrencyTypeId).IsRequired();
        builder.Entity<Bond>().Property(b => b.PrimeRate).IsRequired();
        builder.Entity<Bond>().Property(b => b.StructuredRate).IsRequired();
        builder.Entity<Bond>().Property(b => b.PlacementRate).IsRequired();
        builder.Entity<Bond>().Property(b => b.FloatingRate).IsRequired();
        builder.Entity<Bond>().Property(b => b.CavaliRate).IsRequired();
        builder.Entity<Bond>().HasOne<User>().WithMany().HasForeignKey(b => b.UserId);
        
        builder.Entity<BondMetrics>().HasKey(b => b.Id);
        builder.Entity<BondMetrics>().Property(b => b.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<BondMetrics>().Property(b => b.BondId).IsRequired();
        builder.Entity<BondMetrics>().Property(b => b.MaxPrice).IsRequired();
        builder.Entity<BondMetrics>().Property(b => b.Duration).IsRequired();
        builder.Entity<BondMetrics>().Property(b => b.Convexity).IsRequired();
        builder.Entity<BondMetrics>().Property(b => b.ModifiedDuration).IsRequired();
        builder.Entity<BondMetrics>().Property(b => b.Tcea).IsRequired();
        builder.Entity<BondMetrics>().Property(b => b.Trea).IsRequired();
        builder.Entity<BondMetrics>().HasOne<Bond>().WithMany().HasForeignKey(b => b.BondId);
        
        builder.Entity<CashFlowItem>().HasKey(c => c.Id);
        builder.Entity<CashFlowItem>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<CashFlowItem>().Property(c => c.BondId).IsRequired();
        builder.Entity<CashFlowItem>().Property(c => c.Period).IsRequired();
        builder.Entity<CashFlowItem>().Property(c => c.PaymentDate).IsRequired();
        builder.Entity<CashFlowItem>().Property(c => c.IsGracePeriod).IsRequired();
        builder.Entity<CashFlowItem>().Property(c => c.InitialBalance).IsRequired();
        builder.Entity<CashFlowItem>().Property(c => c.Interest).IsRequired();
        builder.Entity<CashFlowItem>().Property(c => c.Amortization).IsRequired();
        builder.Entity<CashFlowItem>().Property(c => c.FinalBalance).IsRequired();
        builder.Entity<CashFlowItem>().Property(c => c.TotalPayment).IsRequired();
        builder.Entity<CashFlowItem>().Property(c => c.IssuerCashFlow).IsRequired();
        builder.Entity<CashFlowItem>().Property(c => c.BondHolderCashFlow).IsRequired();
        builder.Entity<CashFlowItem>().Property(c => c.PresentCashFlow).IsRequired();
        builder.Entity<CashFlowItem>().Property(c => c.PresentCashFlowTimesPeriod).IsRequired();
        builder.Entity<CashFlowItem>().Property(c => c.ConvexityFactor).IsRequired();
        builder.Entity<CashFlowItem>().HasOne<Bond>().WithMany().HasForeignKey(c => c.BondId);
        
        builder.Entity<CurrencyType>().HasKey(c => c.Id);
        builder.Entity<CurrencyType>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<CurrencyType>().Property(c => c.Currency).IsRequired();
        builder.Entity<CurrencyType>().HasMany<Bond>().WithOne().HasForeignKey(b => b.CurrencyTypeId);
        
        builder.Entity<GracePeriodType>().HasKey(g => g.Id);
        builder.Entity<GracePeriodType>().Property(g => g.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<GracePeriodType>().Property(g => g.GracePeriod).IsRequired();
        builder.Entity<GracePeriodType>().HasMany<Bond>().WithOne().HasForeignKey(b => b.GracePeriodTypeId);
       
        builder.Entity<InterestType>().HasKey(i => i.Id);
        builder.Entity<InterestType>().Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<InterestType>().Property(i => i.Interest).IsRequired();
        builder.Entity<InterestType>().HasMany<Bond>().WithOne().HasForeignKey(b => b.InterestRateTypeId);
        
        // Apply SnakeCase Naming Convention
        builder.UseSnakeCaseWithPluralizedTableNamingConvention();

    }
}
