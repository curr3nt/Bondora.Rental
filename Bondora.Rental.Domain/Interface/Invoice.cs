using System.Collections.Generic;
using System.Linq;

namespace Bondora.Rental.Domain.Interface
{
    public class Invoice
    {
        public readonly string Price;
        public readonly int LoyaltyPoints;
        public readonly List<string> Lines;

        public Invoice(string price, int points, List<string> equipment)
        {
            Price = price;
            LoyaltyPoints = points;
            Lines = equipment;
        }

        public static Invoice Create<TCurrency>(IEnumerable<EquipmentOrder> orderLines, RentalFees<TCurrency> fees) where TCurrency : Currency, new()
        {
            Price<TCurrency> total = new Price<TCurrency>(0, new TCurrency());
            int loyaltyPoints = 0;
            var lines = orderLines.Select(orderLine =>
            {
                var price = orderLine.CalculatePrice(fees);
                total = total.Add(price);
                loyaltyPoints += orderLine.CalculateLoyaltyPoints();
                return price.Print();
            }).ToList();
            return new Invoice(total.Print(), loyaltyPoints, lines);
        }
    }
}
