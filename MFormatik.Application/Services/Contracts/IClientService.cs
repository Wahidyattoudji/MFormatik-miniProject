using MFormatik.Core.DTOs;
using MFormatik.Core.Models;
using System.Collections.ObjectModel;

namespace MFormatik.Application.Services.Contracts
{
    public interface IClientService
    {
        //Task CreateClientAsync(Client client);
        //Task UpdateClientAsync(Client client);
        //Task DeleteClientAsync(Client client);
        Task<ObservableCollection<Client>> GetAllClientsAsync();
        Task<ObservableCollection<ClientDTO>> GetAllClientsASDtoAsync();
    }
}
