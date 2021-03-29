using System.Collections.Generic;

namespace Bondora.Rental.Web.Models
{
    public class InvoiceLine
    {
        public readonly string Name;
        public readonly string Price;

        public InvoiceLine(string name, string price)
        {
            Name = name;
            Price = price;
        }
    }

    public class Invoice
    {
        public readonly IEnumerable<InvoiceLine> Lines;
        public readonly string Price;
        public readonly int LoyaltyPoints;

        public Invoice(IEnumerable<InvoiceLine> lines, string price, int loyaltyPoints)
        {
            Lines = lines;
            Price = price;
            LoyaltyPoints = loyaltyPoints;
        }

        public string ToFile(InvoiceSpec spec) => spec.ToFile(this);
    }
}
