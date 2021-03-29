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
            var sendOptions = new SendOptions();
            sendOptions.SetDestination("Bondora.Rental.Domain.Interface");
            // since now message queue is used, it might be a good idea to "mark" all messages with some unique ID
            // currently I have no confidence how messages from several running web instances will be handled, 
            //   and if NServiceBus does this automatically
            var reply = _messageSession
                .Request<Domain.Interface.Invoice>(order, sendOptions)
                .GetAwaiter()
                .GetResult();
            // expected that invoice keeps equipment in the same order
            // following code will fail if ordering is broken
            var invoiceLines = new List<InvoiceLine>();
            for (var index = 0; index < items.Count; index++)
                invoiceLines.Add(new InvoiceLine(items[index].Name, reply.Lines[index]));

            return new Invoice(invoiceLines, reply.Price, reply.LoyaltyPoints);
        }
    }
}
