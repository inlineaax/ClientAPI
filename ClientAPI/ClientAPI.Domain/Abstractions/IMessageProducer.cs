namespace ClientAPI.Domain.Abstractions
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }
}
