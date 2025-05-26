namespace MFormatik.Core.Models
{
    public class OrderItem : BaseModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? DiscountRate { get; set; }
        public int Position { get; set; }

        // Order 1 - N OrderItem
        public virtual Order Order { get; set; }
        //  Product 1 - N OrderItem
        public virtual Product Product { get; set; }
    }

}
