namespace MFormatik.Core.Models
{
    public class Order : BaseModel
    {
        public int ClientId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal? Total { get; set; }
        public decimal? TotalNet { get; set; }
        public decimal? DiscountRate { get; set; }

        // 1 - N 
        public virtual Client Client { get; set; }

        // Command 1 - N Command Item
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }

}
