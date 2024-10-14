using HRManagement.Data.Interfaces;
using HRManagement.Data.Repositories;
using HRManagement.Domain.DTOs;
using HRManagement.Domain.Entities;
using HRManagement.Domain.Interfaces;

namespace HRManagement.Domain.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            this._clientRepository = clientRepository;  
        }
        public async Task AddAsync(ClientDto clientDto)
        {
            var client = new Client()
            {
                Name = clientDto.Name
            };

            await _clientRepository.AddAsync(client);   
        }

        public async Task<Client>GetByIdAsync(int id)
        {
            return await _clientRepository.GetByIdAsync(id);   
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _clientRepository.GetAllAsync();
        }

        public async Task<Client> DeleteByIdAsync(int id)
        {
            return await _clientRepository.DeleteByIdAsync(id);
        }

        public async Task<Client> UpdateAsync(int id, ClientDto clientDto)
        {
            var existingClient = await _clientRepository.GetByIdAsync(id);
            
            if (existingClient == null)
            {
                throw new KeyNotFoundException($"Client with ID {id} not found");
            }
            existingClient.Name = clientDto.Name;

            return await _clientRepository.UpdateAsync(existingClient);

        }
    }
}
