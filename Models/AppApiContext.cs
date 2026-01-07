using Microsoft.EntityFrameworkCore;
namespace API_FTN_V1._0.Models
{
    public class AppApiContext : DbContext
    {

        public AppApiContext(DbContextOptions<AppApiContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Lot> Lots { get; set; }
    }

}
