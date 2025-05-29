using MFormatik.Application.Services.Contracts;
using MFormatik.Core.Models;

namespace MFormatik.Application.MediatorService
{
    public interface IMediator
    {
        IClientService ClientService { get; }
        IProductService ProductService { get; }
        IOrderService OrderService { get; }

        Order SelectedOrder { get; set; }

        void Subscribe(string message, Action<object> callback);
        void Unsubscribe(string message, Action<object> callback);
        void Notify(string message, object param = null);
    }
}
