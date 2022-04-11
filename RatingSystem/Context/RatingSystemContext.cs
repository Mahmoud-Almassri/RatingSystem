using Microsoft.EntityFrameworkCore;
using RatingSystem.Models;

namespace RatingSystem.Context
{
    public class RatingSystemContext : DbContext
    {
        public RatingSystemContext(DbContextOptions<RatingSystemContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Rate> Rates { get; set; }
    }
}
