namespace RealEstate.Web.Models;

public class CreateAnnouncementRequestModel
{
    public CreateAnnouncementRequestModel()
    {
        Property = new CreatePropertyRequestModel();
    }

    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public CreatePropertyRequestModel Property { get; set; }
}