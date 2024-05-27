using ClientAPI.Domain.Entities;

namespace ClientAPI.Domain.Abstractions
{
    public interface IClientRepository
    {
        Task<Client> AddClient(Client client);
        void UpdateClient(Client client);
        Task<Client> DeleteClient(int clientId);
    }
}
