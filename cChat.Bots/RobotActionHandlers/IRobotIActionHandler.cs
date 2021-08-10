namespace cChat.Bots.RobotActionHandlers
{
    public interface IRobotIActionHandler
    {
        void Process(string message);
        string Key { get; }
    }
}