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
           var content = await GetUrl(message)
               .GetStringFromUrlAsync(accept:"text/csv");
           var quotes = content.FromCsv<IList<StockQuote>>();
            var outputMessage= "";
           if(quotes.Any())
            {
              outputMessage = quotes.First().GetQuote();
            }
            // await SendMessageService.Send(new ParsedChatMessage(){ 
            //     Type = MessageTypes.ErrorMessage,
            // });
        }
    }
}