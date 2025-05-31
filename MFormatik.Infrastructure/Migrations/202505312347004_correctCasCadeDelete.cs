namespace MFormatik.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correctCasCadeDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderItem", "OrderId", "dbo.Order");
            DropForeignKey("dbo.OrderItem", "ProductId", "dbo.Product");
            AddForeignKey("dbo.OrderItem", "OrderId", "dbo.Order", "OrdertId", cascadeDelete: true);
            AddForeignKey("dbo.OrderItem", "ProductId", "dbo.Product", "ProductId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItem", "ProductId", "dbo.Product");
            DropForeignKey("dbo.OrderItem", "OrderId", "dbo.Order");
            AddForeignKey("dbo.OrderItem", "ProductId", "dbo.Product", "ProductId");
            AddForeignKey("dbo.OrderItem", "OrderId", "dbo.Order", "OrdertId");
        }
    }
}
