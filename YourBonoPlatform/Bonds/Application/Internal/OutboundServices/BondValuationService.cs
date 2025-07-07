using YourBonoPlatform.Bonds.Domain.Model.Aggregates;
using YourBonoPlatform.Bonds.Domain.Model.Entities;
using YourBonoPlatform.Bonds.Domain.Model.ValueObjects;
using YourBonoPlatform.Bonds.Domain.Services;

namespace YourBonoPlatform.Bonds.Application.Internal.OutboundServices;

public class BondValuationService : IBondValuationService
{
    private const int MathPrecision = 10;
    private const MidpointRounding Rounding = MidpointRounding.AwayFromZero;

    public async Task<IEnumerable<CashFlowItem>> CalculateCashFlows(Bond bond)
    {
        var totalPeriods = bond.Duration * bond.Frequency;
        var TEA = GetTEA(bond);
        var periodInterestRate = GetPeriodicInterestRate(TEA, bond.Frequency);
        var periodDiscountRate = GetPeriodDiscountRate(bond.DiscountRate / 100m, bond.Frequency);

        List<CashFlowItem> cashFlows = [];
        decimal balance = bond.NominalValue;
        DateTime currentDate = bond.EmissionDate;

        var initialExpensesIssuer = (bond.StructuredRate + bond.PlacementRate + bond.FloatingRate + bond.CavaliRate) / 100m * bond.MarketValue;
        var initialExpensesBondHolder = initialExpensesIssuer;

        var issuerCashFlow = bond.MarketValue - initialExpensesIssuer;
        var bondHolderCashFlow = bond.MarketValue + initialExpensesBondHolder;

        cashFlows.Add(new CashFlowItem(0, bond.Id, 0, currentDate, false, 0, 0, 0, bond.NominalValue, 0, issuerCashFlow, -bondHolderCashFlow, 0, 0, 0));

        for (int period = 1; period <= totalPeriods; period++)
        {
            currentDate = currentDate.AddDays(bond.DaysPerYear / bond.Frequency);
            decimal amortization = 0;
            decimal interest = balance * periodInterestRate;

            if (period == totalPeriods)
            {
                interest = balance * (periodInterestRate + bond.PremiumRate / 100m);
            }

            bool isGrace = period <= bond.GracePeriodDuration;
            if (!isGrace)
            {
                decimal amortizableAmount = cashFlows[bond.GracePeriodDuration].FinalBalance;
                amortization = amortizableAmount / (totalPeriods - bond.GracePeriodDuration);
            }

            decimal totalPayment = 0;
            decimal finalBalance = 0;

            if (isGrace)
            {
                if (bond.GracePeriodTypeId == (int)EGracePeriodTypes.Total)
                {
                    totalPayment = 0;
                    finalBalance = balance + interest;
                    bondHolderCashFlow = 0;
                }
                else if (bond.GracePeriodTypeId == (int)EGracePeriodTypes.Partial)
                {
                    totalPayment = interest;
                    amortization = 0;
                    finalBalance = balance;
                    bondHolderCashFlow = totalPayment;
                }
            }
            else
            {
                totalPayment = interest + amortization;
                finalBalance = balance - amortization;
                bondHolderCashFlow = totalPayment;
            }

            finalBalance = Math.Abs(finalBalance) < 0.00001m ? 0 : finalBalance;
            issuerCashFlow = -bondHolderCashFlow;

            decimal presentValue = GetPresentValue(bondHolderCashFlow, periodDiscountRate, period);
            decimal presentValueTimesPeriod = presentValue * period;
            decimal convexityFactor = presentValueTimesPeriod * (period + 1);

            cashFlows.Add(new CashFlowItem(
                0, bond.Id, period, currentDate, isGrace,
                Math.Round(balance, 2),
                Math.Round(interest, 2),
                Math.Round(amortization, 2),
                Math.Round(finalBalance, 2),
                Math.Round(totalPayment, 2),
                Math.Round(issuerCashFlow, 2),
                Math.Round(bondHolderCashFlow, 2),
                Math.Round(presentValue, 10),
                Math.Round(presentValueTimesPeriod, 10),
                Math.Round(convexityFactor, 10)));

            balance = finalBalance;
        }

        return await Task.FromResult(cashFlows);
    }

    public async Task<BondMetrics?> CalculateBondMetrics(Bond bond, IEnumerable<CashFlowItem> cashFlows)
    {
        decimal periodDiscountRate = GetPeriodDiscountRate(bond.DiscountRate / 100m, bond.Frequency);
        decimal maxBondPrice = GetMaxBondPrice(cashFlows.Select(c => c.PresentCashFlow).ToList());
        decimal duration = GetDuration(cashFlows.Select(c => c.PresentCashFlowTimesPeriod).ToList(), maxBondPrice);
        decimal modifiedDuration = duration / (1 + periodDiscountRate);
        decimal convexity = GetConvexity(cashFlows.Select(c => c.ConvexityFactor).ToList(), maxBondPrice, periodDiscountRate);
        decimal tcea = GetTCEA(cashFlows, bond.Frequency);
        decimal trea = GetTREA(cashFlows, bond.Frequency);
    
        // Nuevos valores
        decimal cok = bond.DiscountRate / 100m;
        decimal netPresentValue = maxBondPrice - bond.MarketValue;
        decimal tceaWithShield = GetTCEAWithShield(cashFlows, bond.Frequency, bond.TaxRate);

        return await Task.FromResult(new BondMetrics(
            0,
            bond.Id,
            maxBondPrice,
            duration,
            convexity,
            modifiedDuration,
            tcea,
            trea,
            netPresentValue,
            cok,
            tceaWithShield
        ));
    }

    private decimal GetTEA(Bond bond)
    {
        if (bond.InterestRateTypeId == (int)EInterestTypes.Effective)
        {
            // Ya es TEA
            return bond.InterestRate / 100m;
        }

        // Si la capitalización es igual a la frecuencia de pago, no se convierte
        if (bond.Capitalization == bond.DaysPerYear / bond.Frequency)
        {
            // La TNA ya es compatible con la frecuencia de pago
            return bond.InterestRate / 100m;
        }

        // Si no, se convierte la TNA a TEA
        decimal tna = bond.InterestRate / 100m;
        int m = bond.DaysPerYear / bond.Capitalization;
        return (decimal)Math.Pow((double)(1 + tna / m), m) - 1;
    }




    private decimal GetPeriodicInterestRate(decimal tea, int frequency)
    {
        double baseVal = (double)(1 + tea);
        double exponent = 1.0 / frequency;
        return (decimal)(Math.Pow(baseVal, exponent) - 1);
    }

    private decimal GetPeriodDiscountRate(decimal discountRate, int frequency)
    {
        if (discountRate == 0)
            return 0;
    
        double baseVal = (double)(1 + discountRate);
        double exponent = 1.0 / frequency;
        return (decimal)(Math.Pow(baseVal, exponent) - 1);
    }


    private decimal GetPresentValue(decimal value, decimal rate, int period)
    {
        return value / (decimal)Math.Pow((double)(1 + rate), period);
    }

    private decimal GetMaxBondPrice(List<decimal> presentCashFlows)
    {
        return presentCashFlows.Skip(1).Sum();
    }

    private decimal GetDuration(List<decimal> flowsTimesPeriod, decimal price)
    {
        return flowsTimesPeriod.Skip(1).Sum() / price;
    }

    private decimal GetConvexity(List<decimal> convexityFactors, decimal price, decimal rate)
    {
        decimal sum = convexityFactors.Skip(1).Sum();
        decimal denom = price * (decimal)Math.Pow((double)(1 + rate), 2);
        return sum / denom;
    }

    private decimal GetTREA(IEnumerable<CashFlowItem> cashFlows, int frequency)
    {
        var flows = cashFlows.Select(c => c.BondHolderCashFlow).ToList();
        decimal trep = CalculateIRR(flows);
        return (decimal)Math.Pow((double)(1 + trep), frequency) - 1;
    }

    private decimal GetTCEA(IEnumerable<CashFlowItem> cashFlows, int frequency)
    {
        var flows = cashFlows.Select(c => c.IssuerCashFlow).ToList();
        decimal tcep = CalculateIRR(flows);
        return (decimal)Math.Pow((double)(1 + tcep), frequency) - 1;
    }

    private decimal CalculateIRR(List<decimal> cashFlows)
    {
        const decimal tol = 1e-10M;
        const int maxIter = 1000;
        decimal guess = 0.1M;

        for (int iter = 0; iter < maxIter; iter++)
        {
            decimal f = 0, df = 0;
            for (int t = 0; t < cashFlows.Count; t++)
            {
                decimal denom = (decimal)Math.Pow((double)(1 + guess), t);
                decimal denom2 = (decimal)Math.Pow((double)(1 + guess), t + 1);
                f += cashFlows[t] / denom;
                df -= t * cashFlows[t] / denom2;
            }

            decimal newGuess = guess - f / df;
            if (Math.Abs(newGuess - guess) < tol)
                return newGuess;

            guess = newGuess;
        }

        throw new Exception("IRR did not converge");
    }
    
    private decimal GetTCEAWithShield(IEnumerable<CashFlowItem> cashFlows, int frequency, decimal taxRate)
    {
        var flows = cashFlows.Select(c => c.IssuerCashFlow).ToList();

        for (int i = 1; i < flows.Count; i++)
        {
            decimal interest = cashFlows.ElementAt(i).Interest;
            flows[i] += interest * taxRate / 100m;
        }

        decimal tcep = CalculateIRR(flows);
        return (decimal)Math.Pow((double)(1 + tcep), frequency) - 1;
    }

}