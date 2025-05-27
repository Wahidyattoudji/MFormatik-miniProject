using HolcimTC.Data.Repositories;
using MFormatik.Core.Contracts;
using MFormatik.Core.Models;

namespace MFormatik.Infrastructure.Repositories
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(IDbContextFactory<MFormatikContext> contextFactory) : base(contextFactory)
        {

        }
    }
}
