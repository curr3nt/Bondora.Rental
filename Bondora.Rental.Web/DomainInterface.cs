using Bondora.Rental.Web.Models;
using NServiceBus;
using System.Collections.Generic;
using System.Linq;

namespace Bondora.Rental.Web
{
    public class DomainInterface
    {
        private readonly IMessageSession _messageSession;

        public DomainInterface(IMessageSession messageSession)
        {
            _messageSession = messageSession;
        }

        public Invoice CalculateInvoice(Cart cart)
        {
            var items = cart.ToList();
            var order = new Domain.Interface.Order
            {
                Equipment = items
                    .Select(item => new Domain.Interface.EquipmentOrder(item.Type, item.RentalDays))
                    .ToList()
            };
            // expected that invoice keeps equipment in the same order
            var sendOptions = new SendOptions();
            sendOptions.SetDestination("Bondora.Rental.Domain.Interface");
            var reply = _messageSession
                .Request<Domain.Interface.Invoice>(order, sendOptions)
                .GetAwaiter()
                .GetResult();
            // following code will fail if ordering is broken
            var invoiceLines = new List<InvoiceLine>();
            for (var index = 0; index < items.Count; index++)
                invoiceLines.Add(new InvoiceLine(items[index].Name, reply.Lines[index]));

            return new Invoice(invoiceLines, reply.Price, reply.LoyaltyPoints);
        }
    }
}
