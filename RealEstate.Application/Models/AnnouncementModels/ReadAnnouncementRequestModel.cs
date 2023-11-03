namespace RealEstate.Application.Models.AnnouncementModels;

public class ReadAnnouncementRequestModel
{
    public string? OrderBy { get; set; }

    public string? SearchText { get; set; }

    public int page { get; set; } = 1;

    public int PageCount { get; set; } = 9;
}