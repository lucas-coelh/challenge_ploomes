using Domain.Interfaces.Generics;
using Entities.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IMessage : IGenerics<Message>
    {
        Task<List<Message>> GetMessagesList(Expression<Func<Message, bool>> exMessage);
    }
}
