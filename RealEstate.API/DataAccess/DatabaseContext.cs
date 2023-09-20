using Microsoft.EntityFrameworkCore;
using RealEstate.API.DataAccess.Users;

namespace RealEstate.API.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Company { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options) { }
    }
}
