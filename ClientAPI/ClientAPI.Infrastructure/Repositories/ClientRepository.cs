using ClientAPI.Domain.Abstractions;
using ClientAPI.Domain.Entities;
using ClientAPI.Infrastructure.Context;
using ClientAPI.Infrastructure.RabbitMQ.Exceptions;

namespace ClientAPI.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        protected readonly AppDbContext db;

        public ClientRepository(AppDbContext _db)
        {
            db = _db;
        }

        public async Task<Client> AddClient(Client client)
        {
            if (client is null)
                throw new ArgumentNullException(nameof(client));

            await db.Clients.AddAsync(client);
            return client;
        }

        public void UpdateClient(Client client)
        {
            if (client is null)
                throw new ClientNotFoundException(client.Id);

            db.Clients.Update(client);
        }

        public async Task<Client> DeleteClient(int clientId)
        {
            var client = await db.Clients.FindAsync(clientId);

            if (client is null)
                throw new ClientNotFoundException(clientId);

            db.Clients.Remove(client);
            return client;
        }
    }
}
