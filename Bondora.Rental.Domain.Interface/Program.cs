using NServiceBus;
using System;
using System.Threading.Tasks;

namespace Bondora.Rental.Domain.Interface
{
    public class Program
    {
        static async Task Main()
        {
            var endpointConfiguration = new EndpointConfiguration("Bondora.Rental.Domain.Interface");
            endpointConfiguration.UsePersistence<LearningPersistence>();
            endpointConfiguration.UseTransport<LearningTransport>();
            endpointConfiguration.EnableCallbacks(makesRequests: false);

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}
