using HolcimTC.Data.Repositories;
using MFormatik.Core.Contracts;
using MFormatik.Core.DTOs;
using MFormatik.Core.Models;
using System.Data.Entity;
using VisaBOT.Core.Extentions;

namespace MFormatik.Infrastructure.Repositories
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {

        private readonly IDbContextFactory<MFormatikContext> _contextFactory;
        private readonly MFormatikContext _context;

        public ClientRepository(IDbContextFactory<MFormatikContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
            _context = _contextFactory.CreateDbContext();
        }

        public async Task<IEnumerable<ClientDTO>> GetAllClientsASDtoAsync()
        {
            try
            {
                return await _context.Clients
                    .Select(c => new ClientDTO
                    {
                        Id = c.Id,
                        FullName = c.FirstName + " " + c.LastName,
                    })
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                ex.LogError();
                return new List<ClientDTO>();
            }
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            try
            {
                return await _context.Clients.AsNoTracking()
                                              .ToListAsync();
            }
            catch (Exception ex)
            {
                ex.LogError();
                return new List<Client>();
            }
        }
    }
}
