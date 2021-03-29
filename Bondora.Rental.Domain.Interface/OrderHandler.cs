using NServiceBus;
using NServiceBus.Logging;
using System.Threading.Tasks;

namespace Bondora.Rental.Domain.Interface
{
    class OrderHandler : IHandleMessages<Order>
    {
        private static readonly ILog _logger = LogManager.GetLogger<OrderHandler>();

        public Task Handle(Order message, IMessageHandlerContext context)
        {
            _logger.Info("Incoming message from client");
            var invoice = Invoice.Create(message.Equipment, RentalFees.EuroRentalPrices);
            return context.Reply(invoice);
        }
    }
}
