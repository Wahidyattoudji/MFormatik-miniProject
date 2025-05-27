using HolcimTC.Core.Interfaces;
using MFormatik.Application.Services.Contracts;


namespace MFormatik.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }

}
