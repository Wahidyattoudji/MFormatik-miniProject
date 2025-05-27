using HolcimTC.Data.Repositories;
using MFormatik.Core.Contracts;
using MFormatik.Core.Models;
using System.Data.Entity.Infrastructure;

namespace MFormatik.Infrastructure.Repositories
{
    public class ClientRepository : BaseRepository<Client>,
        IClientRepository
    {
        public ClientRepository(IDbContextFactory<MFormatikContext> contextFactory) : base(contextFactory)
        {

        }
    }
}
