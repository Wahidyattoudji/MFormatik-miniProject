using MFormatik.Core.Contracts;

namespace HolcimTC.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepo { get; }
        IClientRepository ClientRepo { get; }
        IOrderRepository TrainerRepo { get; }


        Task<bool> IsCanConnect();
        Task<int> SaveChangesAsync();
    }
}
