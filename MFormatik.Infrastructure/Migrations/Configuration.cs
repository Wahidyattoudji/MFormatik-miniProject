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
            context.Products.AddOrUpdate(SeedData.GetProducts().ToArray());

            context.Clients.AddOrUpdate(SeedData.GetClients().ToArray());

            context.Orders.AddOrUpdate(SeedData.GetOrders().ToArray());
            context.OrderItems.AddOrUpdate(SeedData.GetOrderItems().ToArray());
        }
    }
}
