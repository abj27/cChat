using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cChat.Core.DTOs
{
    public class BotAction
    {
        public BotAction(string text)
        {
            Message = null;
            Action = null;
            text = text?.Trim();
            if(text != null && text.Length > 1 && text.IndexOf('/') == 0)
            {
                text = text?.TrimStart('/'); 
                var values =text.Split(' ',2).ToList();
                Action = values.FirstOrDefault();
                Message= values.Skip(1).FirstOrDefault();
            }
        }
        public string Message { get; set; }
        public string Action { get; set; }
    }
}
