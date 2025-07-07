namespace YourBonoPlatform.Bonds.Domain.Model.Entities
{
    public class BondMetrics
    {
        public BondMetrics(
            int id,
            int bondId,
            decimal maxPrice,
            decimal duration,
            decimal convexity,
            decimal modifiedDuration,
            decimal tcea,
            decimal trea,
            decimal netPresentValue,
            decimal cok,
            decimal tceaWithShield)
        {
            Id = id;
            BondId = bondId;
            MaxPrice = maxPrice;
            Duration = duration;
            Convexity = convexity;
            ModifiedDuration = modifiedDuration;
            Tcea = tcea;
            Trea = trea;
            NetPresentValue = netPresentValue;
            Cok = cok;
            TceaWithShield = tceaWithShield;
        }

        public void Update(BondMetrics updatedMetrics)
        {
            Duration = updatedMetrics.Duration;
            Convexity = updatedMetrics.Convexity;
            ModifiedDuration = updatedMetrics.ModifiedDuration;
            Tcea = updatedMetrics.Tcea;
            Trea = updatedMetrics.Trea;
            NetPresentValue = updatedMetrics.NetPresentValue;
            Cok = updatedMetrics.Cok;
            TceaWithShield = updatedMetrics.TceaWithShield;
        }

        public int Id { get; private set; }
        public int BondId { get; private set; }
        public decimal MaxPrice { get; private set; }
        public decimal Duration { get; private set; }
        public decimal Convexity { get; private set; }
        public decimal ModifiedDuration { get; private set; }
        public decimal Tcea { get; private set; }
        public decimal Trea { get; private set; }
        public decimal NetPresentValue { get; private set; }     
        public decimal Cok { get; private set; }                 
        public decimal TceaWithShield { get; private set; }     
    }
}
