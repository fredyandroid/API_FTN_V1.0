using Microsoft.EntityFrameworkCore;
namespace API_FTN_V1._0.Models
{
    public class UserApiContext : DbContext
    {

        public UserApiContext(DbContextOptions<UserApiContext> options)
            : base(options)
        {


        }
        public DbSet<User> Users { get; set; }
    }

}
