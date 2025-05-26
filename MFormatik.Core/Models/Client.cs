namespace MFormatik.Core.Models
{
    public class Client : BaseModel
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        // Client 1 - N Order : 
        public virtual ICollection<Order> Orders { get; set; }
    }

}
