using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;

namespace Domain.Services
{
    public class ServiceMessage : IServiceMessage
    {

        private readonly IMessage _IMessage;

        public ServiceMessage(IMessage IMessage)
        {
            _IMessage = IMessage;
        }
    }
}
