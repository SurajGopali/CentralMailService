namespace CentralMailService.Models
{
    public class EmailQueue
    {
        public int Id { get; set; }                
        public int ProductId { get; set; }       
        public string ToEmail { get; set; }         
        public string? CcEmail { get; set; }     
        public string? BccEmail { get; set; }       
        public string Subject { get; set; }       
        public string Body { get; set; }           
        public int Status { get; set; } = 0;       
        public int RetryCount { get; set; } = 0;
        public DateTime? LastAttemptDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}