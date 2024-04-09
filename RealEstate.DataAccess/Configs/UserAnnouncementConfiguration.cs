using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.DataAccess.Entities;

namespace RealEstate.DataAccess.Configs;

public class UserAnnouncementConfiguration : IEntityTypeConfiguration<UserAnnouncement> 
{
    public void Configure(EntityTypeBuilder<UserAnnouncement> builder)
    {
        builder.HasKey(ua => new { ua.UserId, ua.AnnouncementId });

        builder.HasOne(x => x.Announcement)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.AnnouncementId);
        
        builder.HasOne(x => x.User)
            .WithMany(x => x.FavoritesAnnouncements)
            .HasForeignKey(x => x.UserId);
    }
}