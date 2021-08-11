using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cChat.Core.DTOs
{
    public class StockQuote
    {
        public string Symbol { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Open {get; set;}
        public string High { get; set; }
        public string Low {get; set;}
        public string Close { get; set; }
        public string Volume {get; set;}

        public string GetQuote()
        {
            if (Symbol == null || Close == null || !Decimal.TryParse(Close, out decimal closeValue))
            {
                return null;
            }
            return $"{Symbol} quote is ${String.Format(closeValue % 1 == 0 ? "{0:N0}" : "{0:N2}", closeValue)} per share";
        }
    }
}
