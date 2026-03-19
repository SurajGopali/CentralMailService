namespace CentralMailService.Models
{
    public class SendEmailRequest
    {
        public string ToEmail { get; set; }
        public string? CcEmail { get; set; }
        public string? BccEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string? Signature { get; set; }
        public IFormFile? Attachment { get; set; }
    }
}