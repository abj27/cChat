using cChat.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace cChat.Data.Repositories
{
    public class ChatRoomRepository :Repository<ChatRoom,int > , IChatRoomRepository
    {
        public ChatRoomRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
