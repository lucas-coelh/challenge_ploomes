using Domain.Interfaces;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repos.Generics;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repos.Repositories
{
    public class RepositoryMessage : RepositoryGenerics<Message>, IMessage
    {

        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public RepositoryMessage()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<List<Message>> GetMessagesList(Expression<Func<Message, bool>> exMessage)
        {
            using (var dataBase = new ContextBase(_OptionsBuilder))
            {
                return await dataBase.Message.Where(exMessage).AsNoTracking().ToListAsync();
            }
        }
    }
}
