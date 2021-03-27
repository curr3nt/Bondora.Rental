using System.Collections.Generic;

namespace Bondora.Rental.Domain.Service
{
    public class Service
    {
        public Invoice CalculateOrder(IEnumerable<EquipmentOrder> orderLines) =>
            Invoice.Create(orderLines, RentalFees.EuroRentalPrices);
    }
}
