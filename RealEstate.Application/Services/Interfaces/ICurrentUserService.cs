using Microsoft.AspNetCore.Http;

namespace RealEstate.Application.Services.Interfaces;

public interface ICurrentUserService
{
    public int UserId { get; }
}

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int UserId => int.Parse(_httpContextAccessor.HttpContext.User?.FindFirst("id")?.Value!);
}
