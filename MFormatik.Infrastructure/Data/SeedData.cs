using MFormatik.Core.Models;

namespace MFormatik.Infrastructure.Data
{
    public static class SeedData
    {
        public static List<Client> GetClients()
        {
            return new List<Client>
            {
                new Client { Id = 1, FirstName = "Alice"   ,LastName="Smith", Phone = "123456789", Email = "alice@example.com",  },
                new Client { Id = 2, FirstName = "Bob"     ,LastName="Johnson", Phone = "987654321", Email = "bob@example.com",  },
                new Client { Id = 3, FirstName = "Charlie" ,LastName="Davis", Phone = "555666777", Email = "charlie@example.com", },
                new Client { Id = 4, FirstName = "Diana"   ,LastName="Moore", Phone = "222333444", Email = "diana@example.com", },
                new Client { Id = 5, FirstName = "Eve"     ,LastName="Brown", Phone = "777888999", Email = "eve@example.com" }
            };
        }

        public static List<Product> GetProductSeedData()
        {
            return new List<Product>
            {
                new Product { Id = 1, Name = "Product A", UnitPrice = 10.0m },
                new Product { Id = 2, Name = "Product B", UnitPrice = 20.0m },
                new Product { Id = 3, Name = "Product C", UnitPrice = 15.5m },
                new Product { Id = 4, Name = "Product D", UnitPrice = 7.75m },
                new Product { Id = 5, Name = "Product E", UnitPrice = 12.3m }
            };
        }

        public static List<Order> GetOrderSeedData()
        {
            return new List<Order>
            {
                new Order { Id = 1, ClientId = 1, OrderDate = new DateTime(2025, 05, 01), Total = 100, TotalNet = 90, DiscountRate = 10 },
                new Order { Id = 2, ClientId = 2, OrderDate = new DateTime(2025, 05, 03), Total = 150, TotalNet = 135, DiscountRate = 10 },
                new Order { Id = 3, ClientId = 3, OrderDate = new DateTime(2025, 05, 05), Total = 200, TotalNet = 190, DiscountRate = 5 },
                new Order { Id = 4, ClientId = 4, OrderDate = new DateTime(2025, 05, 07), Total = 80, TotalNet = 72, DiscountRate = 10 },
                new Order { Id = 5, ClientId = 5, OrderDate = new DateTime(2025, 05, 09), Total = 50, TotalNet = 45, DiscountRate = 10 }
            };
        }

        public static List<OrderItem> GetOrderItemSeedData()
        {
            return new List<OrderItem>
            {
                new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 2, UnitPrice = 10, DiscountRate = 5, Position = 1 },
                new OrderItem { Id = 2, OrderId = 1, ProductId = 2, Quantity = 1, UnitPrice = 20, DiscountRate = 0, Position = 2 },
                new OrderItem { Id = 3, OrderId = 2, ProductId = 3, Quantity = 3, UnitPrice = 15.5m, DiscountRate = 10, Position = 1 },
                new OrderItem { Id = 4, OrderId = 3, ProductId = 4, Quantity = 4, UnitPrice = 7.75m, DiscountRate = 0, Position = 1 },
                new OrderItem { Id = 5, OrderId = 4, ProductId = 5, Quantity = 5, UnitPrice = 12.3m, DiscountRate = 5, Position = 1 }
            };
        }

    }
}
