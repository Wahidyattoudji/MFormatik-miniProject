using HolcimTC.Core.Interfaces;
using MFormatik.Application.Services.Contracts;
using MFormatik.Core.DTOs;
using MFormatik.Core.Models;
using System.Collections.ObjectModel;

namespace MFormatik.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ObservableCollection<Client>> GetAllClientsAsync()
        {
            var clients = await _unitOfWork.ClientRepository.GetAllClientsAsync();
            return new ObservableCollection<Client>(clients);
        }

        public async Task<ObservableCollection<ClientDTO>> GetAllClientsASDtoAsync()
        {
            var clientsAsDto = await _unitOfWork.ClientRepository.GetAllClientsASDtoAsync();
            return new ObservableCollection<ClientDTO>(clientsAsDto);
        }
    }
}
