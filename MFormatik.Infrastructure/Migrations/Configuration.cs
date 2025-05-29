namespace MFormatik.Infrastructure.Migrations
{
    using MFormatik.Infrastructure.Data;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<MFormatikContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MFormatikContext context)
        {
            context.Products.AddOrUpdate(p => p.Id, SeedData.GetProducts().ToArray());

            context.Clients.AddOrUpdate(p => p.Id, SeedData.GetClients().ToArray());

            context.Orders.AddOrUpdate(p => p.Id, SeedData.GetOrders().ToArray());
            context.OrderItems.AddOrUpdate(p => p.Id, SeedData.GetOrderItems().ToArray());
        }
    }
}
