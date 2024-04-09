using RealEstate.Application.Models.PropertyModels;
using RealEstate.DataAccess;
using RealEstate.DataAccess.Entities;

namespace RealEstate.Application.Models.AnnouncementModels;

public class AnnouncementResponseModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public PropertyResponseModel Property { get; set; }


    public static AnnouncementResponseModel FromAnnouncement(Announcement announcement)
    {
        return new AnnouncementResponseModel
        {
            Id = announcement.Id,
            Title = announcement.Title,
            StartDate = announcement.StartDate,
            EndDate = announcement.EndDate,
            Property = announcement.Property != null
                ? PropertyResponseModel.FromProperty(announcement.Property)
                : null
        };
    }
}