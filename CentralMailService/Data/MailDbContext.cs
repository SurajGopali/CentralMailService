using Microsoft.EntityFrameworkCore;
using CentralMailService.Models; 

namespace CentralMailService.Data
{
    public class MailDbContext : DbContext
    {
        public MailDbContext(DbContextOptions<MailDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<SmtpSetting> SMTPSettings { get; set; }
        public DbSet<EmailQueue> EmailQueue { get; set; }
    }
}
