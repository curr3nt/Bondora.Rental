using System;
using System.Linq;

namespace Bondora.Rental.Web.Models
{
    public interface InvoiceSpec
    {
        string FileName();
        string ToFile(Invoice invoice);
    }

    public class EnglishInvoice : InvoiceSpec
    {
        public string FileName() => "invoice.txt";

        public string ToFile(Invoice invoice)
        {
            return new[]
            {
                "Invoice generated at " + DateTime.Now,
                "---",
                "Equipment\tPrice",
                "",
            }
            .Concat(invoice.Lines.Select(l => l.Name + "\t" + l.Price))
            .Concat(new[]
            {
                "---",
                "Loyalty points\t" + invoice.LoyaltyPoints,
                "Total price\t" + invoice.Price
            })
            .Aggregate((s1, s2) => s1 + Environment.NewLine + s2);
        }
    }
}
