using HolcimTC.Core.Interfaces;
using MFormatik.Application.Services.Contracts;


namespace MFormatik.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    }

}
