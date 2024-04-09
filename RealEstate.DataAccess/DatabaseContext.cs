using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RealEstate.DataAccess.Configs;
using RealEstate.DataAccess.Entities;

namespace RealEstate.DataAccess;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    { }

    public DbSet<User> Users { get; set; }

    public DbSet<Company> Companies { get; set; }

    public DbSet<Announcement> Announcements { get; set; }

    public DbSet<UserAnnouncement> UsersAnnouncements { get; set; }

    public DbSet<Property> Properties { get; set; }

    public DbSet<Adress> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(UserConfiguration))!);
        
        base.OnModelCreating(modelBuilder);
    }
}