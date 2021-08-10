namespace cChat.Bots.RobotActionHandlers
{
    public class StockQuotesActionHandler: IRobotIActionHandler
    {
        public StockQuotesActionHandler(string key)
        {
            Key = key;
        }

        public void Process(string message)
        {
            throw new System.NotImplementedException();
        }

        public string Key { get; }
    }
}