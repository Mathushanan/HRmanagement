using HRManagement.Domain.Entities;

namespace HRManagement.Data.Interfaces
{
    public interface IClientRepository
    {
        Task AddAsync(Client client);
        Task<Client> GetByIdAsync(int id);
        Task<List<Client>> GetAllAsync();
        Task<Client> DeleteByIdAsync(int id);
        Task<Client> UpdateAsync(Client client);
    }
}
