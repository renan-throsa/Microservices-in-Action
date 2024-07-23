namespace ProductCatalog.Queues
{
    public interface IQueue
    {
        public void Publish(Message message);
    }
}
