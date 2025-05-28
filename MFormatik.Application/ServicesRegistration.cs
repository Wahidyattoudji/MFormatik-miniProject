using MFormatik.Application.MediatorService;
using MFormatik.Application.Services;
using MFormatik.Application.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace MFormatik.Application
{
    public static class ServicesRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IClientService, ClientService>();

            services.AddScoped<IMediator, Mediator>();
        }
    }
}
