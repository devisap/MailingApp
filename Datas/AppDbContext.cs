using MailingApp.Models.Entites;
using Microsoft.EntityFrameworkCore;

namespace MailingApp.Datas
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Zone> Zones { get; set; }
    }
}