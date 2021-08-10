using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cChat.Bots.RobotActionHandlers;
using cChat.Core.DTOs;

namespace cChat.Bots.Services
{
    public class ActionHandlerService: IActionHandlerService
    {
        private readonly IList<IRobotIActionHandler> _robotActionHandlers;

        public ActionHandlerService(IList<IRobotIActionHandler> robotActionHandlers)
        {
            _robotActionHandlers = robotActionHandlers;
        }

        public void Process(BotAction botAction)
        {
           var actionHandler = _robotActionHandlers
               .FirstOrDefault(x => x.Key == botAction.Action)?? 
                               _robotActionHandlers.First(x => x.Key == null);
            actionHandler.Process(botAction.Message);
        }
    }

    public interface IActionHandlerService
    {
        void Process(BotAction botAction);
    }
}
