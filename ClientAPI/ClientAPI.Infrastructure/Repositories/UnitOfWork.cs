using ClientAPI.Domain.Abstractions;
using ClientAPI.Infrastructure.Context;
using ClientAPI.Infrastructure.Repositories.MongoDB;
using MongoDB.Driver;

namespace ClientAPI.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IClientRepository? _clientRepo;
        private readonly AppDbContext _context;
        private IMongoClientRepository _mongoClientRepo;
        private readonly IMongoDatabase _mongoDatabase;

        public UnitOfWork(AppDbContext context, IMongoDatabase mongoDatabase)
        {
            _context = context;
            _mongoDatabase = mongoDatabase;
        }

        public IClientRepository ClientRepository
        {
            get
            {
                return _clientRepo = _clientRepo ??
                    new ClientRepository(_context);
            }
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public IMongoClientRepository MongoClientRepository
        {
            get
            {
                return _mongoClientRepo = _mongoClientRepo ??
                    new MongoClientRepository(_mongoDatabase);
            }
        }
    }
}
