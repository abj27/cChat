using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cChat.Data.Repositories;

namespace cChat.BusinessLogic.Services
{
    public interface IChatRoomService
    {
        void GetAll();
    }

    public class ChatRoomService : IChatRoomService
    {
        private readonly IChatRoomRepository _chatRoomRepository;

        public ChatRoomService(IChatRoomRepository chatRoomRepository)
        {
            _chatRoomRepository = chatRoomRepository;
        }
        public  void GetAll()
        {
            _chatRoomRepository.GetAll();
        }
    }
}
