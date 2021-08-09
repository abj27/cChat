using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cChat.Core.DTOs
{
    public class BotAction
    {
        public BotAction(string message)
        {
            Message = message;
        }
        public string Message { get; }
    }
}
