using RealEstate.Application.Models.UsersModels;
using RealEstate.Shared.Models.Users;

namespace RealEstate.Application.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UsersResponseModel>> RealAllUsersAsync();
    Task<UsersResponseModel?> GetUserByIdAsync(int id, bool includeCompanyDetails);
    Task<UsersResponseModel> GetUserByNameAsync(string userName, bool includeCompanyDetails);
    Task<UsersResponseModel> CreateUserAsync(CreateUsersRequestModel requestModel);
    Task<UsersResponseModel> UpdateUserAsync(int id, CreateUsersRequestModel requestModel);
    Task<LoginResponseModel> LoginAsync(LoginRequestModel requestModel);


    Task DeleteUserAsync(int id);

    Task<bool> isEmailUniqueAsync(string email);

    Task<int?> AddAnnouncementToFavoriteAsync(int userId, int announcementId);
}