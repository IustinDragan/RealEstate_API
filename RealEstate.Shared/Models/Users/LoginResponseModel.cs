namespace RealEstate.Shared.Models.Users;

public class LoginResponseModel
{
    public string Username { get; set; }

    public int Id { get; set; }

    public string Email { get; set; }

    public string Token { get; set; }
}