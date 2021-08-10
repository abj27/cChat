using System.Threading.Tasks;

namespace cChat.Bots.RobotActionHandlers
{
    public interface IRobotIActionHandler
    {
        string Key { get; }
        Task Process(string message);
    }
}