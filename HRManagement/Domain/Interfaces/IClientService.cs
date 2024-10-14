using HRManagement.Domain.DTOs;
using HRManagement.Domain.Entities;

namespace HRManagement.Domain.Interfaces
{
    public interface IClientService
    {
        Task AddAsync(ClientDto clientDto);
        Task<Client> GetByIdAsync(int id);
        Task<List<Client>> GetAllAsync();
        Task<Client> DeleteByIdAsync(int id);
        Task<Client> UpdateAsync(int id, ClientDto updatedClientDto);
    }
}
