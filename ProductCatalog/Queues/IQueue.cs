namespace ProductCatalog.Queues
{
    public interface IQueue
    {
        public Task Publish(string sub, string data);
    }
}
