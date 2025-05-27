using Microsoft.Extensions.DependencyInjection;

namespace MFormatik.Infrastructure
{
    public static partial class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            // i used to work with this in ef Core
            //services.AddDbContextFactory<MFormatikContext>(options =>
            //    options.UseSqlServer(connectionString)
            //    .LogTo(message => Trace.WriteLine(message), LogLevel.Information));

            services.AddScoped<IDbContextFactory<MFormatikContext>>(provider =>
                new DbContextFactory<MFormatikContext>(connectionString));

            // Register repositories
            // services.AddScoped<IFormationRepository, FormationRepository>();

            // Register Unit of Work
            //services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}
