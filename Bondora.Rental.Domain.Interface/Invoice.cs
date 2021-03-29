using NServiceBus;
using System.Collections.Generic;
using System.Linq;

namespace Bondora.Rental.Domain.Interface
{
    public class Invoice : IMessage
    {
        public string Price { get; set; }
        public int LoyaltyPoints { get; set; }
        public List<string> Lines { get; set; }

        public Invoice() { }

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
