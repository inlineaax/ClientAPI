using ClientAPI.Domain.Entities;

namespace ClientAPI.Domain.Abstractions
{
    public interface IMongoClientRepository
    {
        Task ClearCollectionAsync();
        Task<Client> GetClientByEmailAsync(string email);
        Task AddClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(int clientId);
        Task<Client> GetClientByIdAsync(int clientId);
        Task<IEnumerable<Client>> GetClientsAsync();
    }
}
