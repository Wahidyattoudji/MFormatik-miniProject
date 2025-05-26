namespace MFormatik.Core.Models
{
    public class BaseModel
    {
        public int Id { get; set; }

        // In Big Project i use this For Audit 

        // public DateTime creatDate { get; set; }
        // public DateTime updatedAt { get; set; }

        // In Critical Projects i use this For Soft Delete + ( EF Interceptors Configuration)

        // public bool IsDeleted { get; set; } = false;
    }
}
