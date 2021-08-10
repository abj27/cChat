using cChat.Data.Entities;

namespace cChat.Data.Repositories
{
    public interface IChatMessageRepository:IRepository<ChatMessage, long>
    {
    }
}