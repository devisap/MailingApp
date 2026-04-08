// using MailingApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MailingApp.Datas
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}