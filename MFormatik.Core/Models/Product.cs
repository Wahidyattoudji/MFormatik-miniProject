namespace MFormatik.Core.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }

        // 1 - N OrderItem
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }

}
