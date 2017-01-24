namespace GetReposList.Queue
{
    public interface IMessageHandler
    {
        void Handle(string message);
    }
}
