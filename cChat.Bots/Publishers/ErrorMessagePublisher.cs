using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cChat.Bots.Publishers
{
    public class ErrorMessageAction
    {
        public ErrorMessageAction(string text)
        {
            Text = text;
        }
        public string Text { get;  }
    }
}
