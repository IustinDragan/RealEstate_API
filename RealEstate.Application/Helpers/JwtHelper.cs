using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RealEstate.DataAccess;

namespace RealEstate.Application.Helpers;

public class JwtHelper
{
    public static string GenerateToken(User user, string secretKey)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>
        {
            new("id", user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role.ToString())
        };
//Din appsetings se citeste cu ConfigurationManager
        // if (user.FirstName.Contains("admin"))
        // {
        //     claims.Add(new Claim(ClaimTypes.Role, "Admin"));
        // }

        var claimIdentity = new ClaimsIdentity(claims);

        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = claimIdentity,
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecretKey")),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescription);

        return tokenHandler.WriteToken(token);
    }
}