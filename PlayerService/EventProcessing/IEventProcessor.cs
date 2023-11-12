namespace PlayersService.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}