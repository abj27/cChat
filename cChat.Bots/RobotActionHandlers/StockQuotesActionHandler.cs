using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using cChat.Bots.Services;
using cChat.Core.DTOs;
using ServiceStack;

namespace cChat.Bots.RobotActionHandlers
{
    public class StockQuotesActionHandler:RobotActionHandler, IRobotIActionHandler
    {
        public StockQuotesActionHandler(ISendMessageService sendMessageService) : base(sendMessageService)
        {
        }
        public override string Key => "stock" ;
        public string GetUrl(string query)
        {
            return $@"https://stooq.com/q/l/?s={query}&f=sd2t2ohlcv&h&e=csv";
        }

        public override async Task HandleMessage(string message)
        {
            var content = await GetUrl(message) .GetStringFromUrlAsync(accept:"text/csv");
            var quotes = content.FromCsv<IList<StockQuote>>();
            if (quotes.Any())
            {
                await SendQuoteMessage(quotes, message);
            }
            else
            { 
                await SendErrorMessage(message);
            }
        }

        private async Task SendErrorMessage(string message)
        {
            await SendMessageService.Send(new ParsedChatMessage()
            {
                Type = MessageTypes.ErrorMessage,
                SenderName = Key,
                Text = $"We couldn't find a quote for {message}"
            });
        }

        private  async Task SendQuoteMessage(IList<StockQuote> quotes, string message)
        {
            var outputMessage = quotes.First().GetQuote();
            if (!string.IsNullOrWhiteSpace(outputMessage))
            {
                await SendMessageService.Send(new ParsedChatMessage()
                {
                    Type = MessageTypes.ChatMessage,
                    SenderName = Key,
                    Text =outputMessage
                });
            }
            else
            {
                await SendErrorMessage(message);
            }
        }
    }
}