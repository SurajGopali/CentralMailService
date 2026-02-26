using CentralMailService.Data;
using CentralMailService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CentralMailService.Controllers.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailApiController : ControllerBase
    {
        private readonly MailDbContext _context;

        public EmailApiController(MailDbContext context)
        {
            _context = context;
        }

        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request)
        {
            var apiKey = Request.Headers["x-api-key"].FirstOrDefault();
            if (string.IsNullOrEmpty(apiKey))
                return Unauthorized("API key missing");

            var product = await _context.Products.FirstOrDefaultAsync(p => p.ApiKey == apiKey && p.IsActive);
            if (product == null)
                return Unauthorized("Invalid API key");

            var email = new EmailQueue
            {
                ProductId = product.Id,
                ToEmail = request.ToEmail,
                CcEmail = request.CcEmail,
                BccEmail = request.BccEmail,
                Subject = request.Subject,
                Body = request.Body,
            };

            _context.EmailQueue.Add(email);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Email queued successfully", EmailId = email.Id });
        }
    }
}
