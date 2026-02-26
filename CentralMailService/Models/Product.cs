namespace CentralMailService.Models
{
    public class Product
    {
        public int Id { get; set; }                 
        public string ProductName { get; set; }    
        public string ApiKey { get; set; }          
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}