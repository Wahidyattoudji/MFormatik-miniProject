using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace MFormatik.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextFactory<MFormatikContext>(options =>
                options.UseSqlServer(connectionString)
                .LogTo(message => Trace.WriteLine(message), LogLevel.Information));

            // Register repositories
            // services.AddScoped<IFormationRepository, FormationRepository>();

            // Register Unit of Work
            //services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}
