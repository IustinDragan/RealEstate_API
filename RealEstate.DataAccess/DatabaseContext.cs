using Microsoft.EntityFrameworkCore;

namespace RealEstate.DataAccess;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Company> Company { get; set; }
    public DbSet<Announcement> Announcement { get; set; }
    public DbSet<UserAnnouncement> UserAnnouncement { get; set; }
    public DbSet<Property> Property { get; set; }
    public DbSet<Adress> Adress { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserAnnouncement>()
            .HasKey(ua => new { ua.UserId, ua.AnnouncementId });

        base.OnModelCreating(modelBuilder);
    }
}