using HolcimTC.Core.Interfaces;
using MFormatik.Application.Services.Contracts;

namespace MFormatik.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    }
}
