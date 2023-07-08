using System.Threading.Tasks;

namespace ServiceBusReceiver
{
    public interface IServiceBusReceiver
    {
        Task<string> ReceiveMessage();
    }
}