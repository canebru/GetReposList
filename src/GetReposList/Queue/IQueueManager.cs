namespace GetReposList.Queue
{
    public interface IQueueManager
    {
        void PublishMessage(string message);
    }
}
