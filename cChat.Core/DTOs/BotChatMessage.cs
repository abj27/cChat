namespace cChat.Core.DTOs
{
    public class BotChatMessage
    {
        public BotChatMessage(string message)
        {
            Message = message;
        }
        public string Message { get; }
    }
}