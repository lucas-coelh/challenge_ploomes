using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;

namespace Domain.Services
{
    public class ServiceMessage : IServiceMessage
    {
        private readonly IMessage _IMessage;

        public ServiceMessage(IMessage IMessage)
        {
            _IMessage = IMessage;
        }

        public async Task Add(Message item)
        {
            var validateTitle = item.ValidPropertyString(item.Title, "Title");
            if (validateTitle)
            {
                item.CreatedDate = DateTime.Now;
                item.ModifiedDate = DateTime.Now;
                item.Active = true;
                await _IMessage.Add(item);
            }
        }

        public async Task Update(Message item)
        {
            var validateTitle = item.ValidPropertyString(item.Title, "Titulo");
            if (validateTitle)
            {
                item.ModifiedDate = DateTime.Now;
                await _IMessage.Update(item);
            }
        }

        public async Task<List<Message>> GetActiveMessagesList(bool active)
        {
            return await _IMessage.GetMessagesList(m => m.Active == active);
        }
    }
}