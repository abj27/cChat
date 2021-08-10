using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cChat.Data.Entities;

namespace cChat.Data.Repositories
{
    public interface IChatMessageRepository:IRepository<ChatMessage, long>
    {
    }

    public class ChatMessageRepository: Repository<ChatMessage,long>, IChatMessageRepository
    {
        public ChatMessageRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
