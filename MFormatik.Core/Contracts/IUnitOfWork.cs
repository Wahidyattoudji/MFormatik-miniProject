using MFormatik.Core.Contracts;

namespace HolcimTC.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        IClientRepository ClientRepository { get; }
        IOrderRepository OrderRepository { get; }

        Task<bool> IsCanConnect();
        Task SaveChangesAsync();
    }
}
