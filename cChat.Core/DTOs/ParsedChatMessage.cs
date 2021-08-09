namespace cChat.Core.DTOs
{
    public class ParsedChatMessage
    {
        public MessageTypes Type { get; set; }
        public string Sender { get; set; }
        public string SenderName { get; set; }
        public string Text { get; set; }
    }
}