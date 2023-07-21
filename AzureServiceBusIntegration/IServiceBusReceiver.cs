using System.Threading.Tasks;

namespace AzureServiceBusIntegration
{
    public interface IServiceBusReceiver
    {
        Task<Content> ReceiveMessage();
    }
}