using ClientAPI.Domain.Abstractions;
using ClientAPI.Domain.Entities;
using MongoDB.Driver;

namespace ClientAPI.Infrastructure.Repositories.MongoDB
{
    public class MongoClientRepository : IMongoClientRepository
    {
        private readonly IMongoCollection<Client> _clients;

        public MongoClientRepository(IMongoDatabase database)
        {
            _clients = database.GetCollection<Client>("Clients");
        }

        public async Task ClearCollectionAsync()
        {
            await _clients.DeleteManyAsync(Builders<Client>.Filter.Empty);
        }

        public async Task<Client> GetClientByEmailAsync(string email)
        {
            var filter = Builders<Client>.Filter.Eq(c => c.Email, email);
            return await _clients.Find(filter).FirstOrDefaultAsync();
        }

        public async Task AddClientAsync(Client client)
        {
            await _clients.InsertOneAsync(client);
        }

        public async Task UpdateClientAsync(Client client)
        {
            var filter = Builders<Client>.Filter.Eq(c => c.Id, client.Id);
            await _clients.ReplaceOneAsync(filter, client);
        }

        public async Task DeleteClientAsync(int clientId)
        {
            var filter = Builders<Client>.Filter.Eq(c => c.Id, clientId);
            await _clients.DeleteOneAsync(filter);
        }

        public async Task<Client> GetClientByIdAsync(int clientId)
        {
            return await _clients.Find(c => c.Id == clientId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Client>> GetClientsAsync()
        {
            return await _clients.Find(c => true).ToListAsync();
        }
    }
}
