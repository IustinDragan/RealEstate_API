using Microsoft.EntityFrameworkCore;
using RealEstate.DataAccess.Users;

namespace RealEstate.DataAccess;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Company> Company { get; set; }
}