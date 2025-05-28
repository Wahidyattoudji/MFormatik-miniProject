using HolcimTC.Core.Interfaces;
using MFormatik.Application.Services.Contracts;
using MFormatik.Core.Models;
using System.Collections.ObjectModel;

namespace MFormatik.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ObservableCollection<Product>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllProductsAsync();
            return new ObservableCollection<Product>(products);
        }
    }

}
