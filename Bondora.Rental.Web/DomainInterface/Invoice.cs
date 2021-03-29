using System;
using System.Collections.Generic;
using System.Linq;

namespace Bondora.Rental.Web.DomainInterface
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
        private readonly IEnumerable<InvoiceLine> _lines;
        private readonly string _price;
        private readonly int _loyaltyPoints;

        public Invoice(IEnumerable<InvoiceLine> lines, string price, int loyaltyPoints)
        {
            _lines = lines;
            _price = price;
            _loyaltyPoints = loyaltyPoints;
        }

        public string ToFile()
        {
            return new[]
            {
                "Invoice generated at " + DateTime.Now,
                "---",
                "Equipment\tPrice",
                "",
            }
            .Concat(_lines.Select(l => l.Name + "\t" + l.Price))
            .Concat(new []
            {
                "---",
                "Loyalty points collected\t" + _loyaltyPoints,
                "Total price\t" + _price
            })
            .Aggregate((s1, s2) => s1 + Environment.NewLine + s2);
        }
    }
}
