namespace ProductCatalog.Queues
{
    public class MockQueue : IQueue
    {
        public Task Publish(string sub, string data)
        {
            return Task.CompletedTask;
        }
    }
}
