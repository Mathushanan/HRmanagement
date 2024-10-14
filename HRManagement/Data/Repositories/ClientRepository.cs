using HRManagement.Data.Contexts;
using HRManagement.Data.Interfaces;
using HRManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly HRManagementDbContext _context;

        public ClientRepository(HRManagementDbContext context)
        {
            this._context = context;   
        }
        public async Task AddAsync(Client client)
        {
            await _context.Set<Client>().AddAsync(client);
            await _context.SaveChangesAsync();
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            return await _context.Clients
           .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _context.Set<Client>().ToListAsync();
        }

        public async Task<Client> DeleteByIdAsync(int id)
        {
            var client = await _context.Set<Client>().FindAsync(id);
            
            if (client == null)
            {
                throw new KeyNotFoundException($"Client with ID {id} not found.");
            }

            _context.Set<Client>().Remove(client);
            await _context.SaveChangesAsync();

            return client;
        }

        public async Task<Client> UpdateAsync(Client client)
        {
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return client;
        }
    }
}
