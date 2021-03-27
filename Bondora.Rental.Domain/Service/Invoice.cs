using System.Collections.Generic;
using System.Linq;

namespace Bondora.Rental.Domain.Service
{
    public class InvoiceLine
    {
        public readonly string Price;
        public readonly int LoyaltyPoints;

        public InvoiceLine(string price, int loyaltyPoints)
        {
            Price = price;
            LoyaltyPoints = loyaltyPoints;
        }
    }

    public class Invoice
    {
        public readonly string Price;
        public readonly List<InvoiceLine> Lines;

        public Invoice(string price, List<InvoiceLine> equipment)
        {
            Price = price;
            Lines = equipment;
        }

        public static Invoice Create<TCurrency>(IEnumerable<EquipmentOrder> orderLines, RentalFees<TCurrency> fees) where TCurrency : Currency, new()
        {
            Price<TCurrency> total = new Price<TCurrency>(0, new TCurrency());
            var lines = orderLines.Select(orderLine =>
            {
                var price = orderLine.CalculatePrice(fees);
                total = total.Add(price);
                return new InvoiceLine(price.Print(), orderLine.CalculateLoyaltyPoints());
            }).ToList();
            return new Invoice(total.Print(), lines);
        }
    }
}
