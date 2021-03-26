namespace Bondora.Rental.Domain
{
    // rental prices is just a specific set of Price<T> values
    public class RentalFees<TCurrency> where TCurrency : Currency
    {
        public Price<TCurrency> OneTime { get; }
        public Price<TCurrency> PremiumDaily { get; }
        public Price<TCurrency> RegularDaily { get; }

        public RentalFees(Price<TCurrency> oneTime, Price<TCurrency> premiumDaily, Price<TCurrency> regularDaily)
        {
            OneTime = oneTime;
            PremiumDaily = premiumDaily;
            RegularDaily = regularDaily;
        }
        
        public static readonly RentalFees<Euro> EuroRentalPrices = 
            new RentalFees<Euro>(Euro.CreatePrice(100), Euro.CreatePrice(60), Euro.CreatePrice(40));
    }
}
