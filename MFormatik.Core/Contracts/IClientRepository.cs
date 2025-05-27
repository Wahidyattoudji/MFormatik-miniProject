using MFormatik.Core.DTOs;
using MFormatik.Core.Models;


namespace MFormatik.Core.Contracts
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<IEnumerable<ClientDTO>> GetAllClientsASDtoAsync();
    }
}
