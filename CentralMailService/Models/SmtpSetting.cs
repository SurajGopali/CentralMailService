namespace CentralMailService.Models
{
    public class SmtpSetting
    {
        public int Id { get; set; }                 
        public int ProductId { get; set; }         
        public string SmtpHost { get; set; }       
        public int SmtpPort { get; set; }          
        public string SmtpUser { get; set; }      
        public string SmtpPassword { get; set; }   
        public string FromEmail { get; set; }       
        public string FromName { get; set; }       
        public string? DefaultCcEmail { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
    }
}