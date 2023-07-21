using System.Threading.Tasks;

namespace AzureServiceBusIntegration
{
    public interface IServiceBusSender
    {
        Task<bool> SendMessage(string content);

        Task<bool> SendMessage<T>(T content);
    }
}