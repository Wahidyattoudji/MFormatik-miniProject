using HolcimTC.Core.Interfaces;
using MFormatik.Core.Contracts;
using MFormatik.Infrastructure.Repositories;

namespace MFormatik.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContextFactory<MFormatikContext> _contextFactory;
        private MFormatikContext _context;

        public IProductRepository ProductRepository { get; private set; }
        public IClientRepository ClientRepository { get; private set; }
        public IOrderRepository OrderRepository { get; private set; }

        public UnitOfWork(IDbContextFactory<MFormatikContext> contextFactory)
        {
            _contextFactory = contextFactory;
            _context = _contextFactory.CreateDbContext();

            ProductRepository = new ProductRepository(_contextFactory);
            ClientRepository = new ClientRepository(_contextFactory);




        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<bool> IsCanConnect()
        {
            try
            {
                var connection = _context.Database.Connection;
                if (connection.State != System.Data.ConnectionState.Open)
                    await connection.OpenAsync();

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (_context.Database.Connection.State == System.Data.ConnectionState.Open)
                    _context.Database.Connection.Close();
            }
        }
    }
}
