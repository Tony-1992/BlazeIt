using BlazeIt.Server.Extensions;
using BlazeIt.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazeIt.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _config;
        public ApplicationDbContext(IConfiguration config)
        {
            _config = config;
        }

        // DB tables
        public DbSet<User> TABLE_Users { get; set; }
        public DbSet<Todo> TABLE_Todos { get; set; }
        public DbSet<Feedback> TABLE_Feedbacks { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
        }

    }
}
