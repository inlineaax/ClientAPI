namespace ClientAPI.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IClientRepository ClientRepository { get; }
        IMongoClientRepository MongoClientRepository { get; }
        Task CommitAsync();
    }
}
