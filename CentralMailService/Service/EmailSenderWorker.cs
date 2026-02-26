using CentralMailService.Data;
using CentralMailService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace CentralMailService.Services
{
    public class EmailSenderWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public EmailSenderWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<MailDbContext>();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                var pendingEmails = await db.EmailQueue
                    .Where(e => e.Status == 0)
                    .OrderBy(e => e.CreatedDate)
                    .Take(50)
                    .ToListAsync();

                foreach (var email in pendingEmails)
                {
                    var smtp = await db.SMTPSettings.FirstOrDefaultAsync(s => s.ProductId == email.ProductId && s.IsActive);
                    if (smtp == null) continue;

                    bool success = await emailService.SendEmailAsync(email, smtp);

                    email.Status = success ? 1 : 2;
                    email.RetryCount += success ? 0 : 1;
                    email.LastAttemptDate = DateTime.UtcNow;
                }

                await db.SaveChangesAsync();
                await Task.Delay(5000, stoppingToken); 
            }
        }
    }
}