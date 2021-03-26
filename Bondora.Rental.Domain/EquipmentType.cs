namespace Bondora.Rental.Domain
{
    interface EquipmentType
    {
        int LoyaltyPoints { get; }
        Price<TCurrency> CalculatePrice<TCurrency>(RentalFees<TCurrency> rentalFees, int rentalDays) where TCurrency : Currency;
    }

    public class HeavyEquiment : EquipmentType
    {
        public int LoyaltyPoints => 2;

        public Price<TCurrency> CalculatePrice<TCurrency>(RentalFees<TCurrency> rentalFees, int rentalDays) where TCurrency : Currency
        {
            return rentalFees.OneTime + rentalFees.PremiumDaily * rentalDays;
        }
    }

    public class RegularEquipment : EquipmentType
    {
        public int LoyaltyPoints => 1;

        public Price<TCurrency> CalculatePrice<TCurrency>(RentalFees<TCurrency> rentalFees, int rentalDays) where TCurrency : Currency
        {
            if (rentalDays <= 2)
                return 
                    rentalFees.OneTime 
                    + rentalFees.PremiumDaily * rentalDays;
            else
                return 
                    rentalFees.OneTime 
                    + rentalFees.PremiumDaily * 2 
                    + rentalFees.RegularDaily * (rentalDays - 2);
        }
    }

    public class SpecializedEquipment : EquipmentType
    {
        public int LoyaltyPoints => 1;

        public Price<TCurrency> CalculatePrice<TCurrency>(RentalFees<TCurrency> rentalFees, int rentalDays) where TCurrency : Currency
        {
            if (rentalDays <= 3)
                return rentalFees.PremiumDaily * rentalDays;
            else
                return rentalFees.PremiumDaily * 3 + rentalFees.RegularDaily * (rentalDays - 3);
        }
    }
}
