using System.Collections.Generic;
using System.Linq;
using Bondora.Rental.Web.Equipment;
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

        public Invoice CalculateInvoice(List<EquipmentOrder> orderLines, EquipmentCollection equipment)
        {
            var typeIndex = equipment.ToDictionary(e => e.Name, e => e.Type);
            var converted = orderLines
                .Select(ol => new Domain.Interface.EquipmentOrder(typeIndex[ol.Name], ol.RentalDays));
            // expected that invoice keeps equipment in the same order
            Domain.Interface.Invoice interfaceInvoice = _service.CalculateInvoice(converted);
            // following code will fail if ordering is broken
            var invoiceLines = new List<InvoiceLine>();
            for (var index = 0; index < orderLines.Count; index++)
                invoiceLines.Add(new InvoiceLine(orderLines[index].Name, interfaceInvoice.Lines[index]));

            return new Invoice(invoiceLines, interfaceInvoice.Price, interfaceInvoice.LoyaltyPoints);
        }
    }
}
