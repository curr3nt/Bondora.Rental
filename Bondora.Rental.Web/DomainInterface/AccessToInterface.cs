using System.Collections.Generic;
using System.Linq;
using Bondora.Rental.Web.Models;

namespace Bondora.Rental.Web.DomainInterface
{
    public class AccessToInterface
    {
        // TODO: replace explicit service reference
        private readonly Domain.Interface.Service _service;

        public AccessToInterface()
        {
            _service = new Domain.Interface.Service();
        }

        public Invoice CalculateInvoice(Cart cart)
        {
            var items = cart.ToList();
            var converted = items.Select(item => new Domain.Interface.EquipmentOrder(item.Type, item.RentalDays));
            // expected that invoice keeps equipment in the same order
            Domain.Interface.Invoice interfaceInvoice = _service.CalculateInvoice(converted);
            // following code will fail if ordering is broken
            var invoiceLines = new List<InvoiceLine>();
            for (var index = 0; index < items.Count; index++)
                invoiceLines.Add(new InvoiceLine(items[index].Name, interfaceInvoice.Lines[index]));

            return new Invoice(invoiceLines, interfaceInvoice.Price, interfaceInvoice.LoyaltyPoints);
        }
    }
}
