using System.Collections.Generic;

namespace Bondora.Rental.Domain.Interface
{
    public class Service
    {
        public Invoice CalculateInvoice(IEnumerable<EquipmentOrder> orderLines) =>
            Invoice.Create(orderLines, RentalFees.EuroRentalPrices);
    }
}
