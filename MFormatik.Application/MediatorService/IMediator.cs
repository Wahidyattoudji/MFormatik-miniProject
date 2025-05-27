using MFormatik.Application.Services.Contracts;

namespace MFormatik.Application.MediatorService
{
    public interface IMediator
    {
        IClientService ClientService { get; }
        IProductService ProductService { get; }
        IOrderService OrderService { get; }

        void Subscribe(string message, Action<object> callback);
        void Unsubscribe(string message, Action<object> callback);
        void Notify(string message, object param = null);
    }
}
