using Entities.Entities;

namespace Domain.Interfaces.InterfaceServices
{
    public interface IServiceMessage
    {
        Task Add(Message Objet);

        Task Update(Message Objet);

        Task<List<Message>> GetActiveMessagesList(bool active);
    }
}