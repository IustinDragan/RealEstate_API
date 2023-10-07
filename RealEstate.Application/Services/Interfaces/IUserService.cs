using RealEstate.Application.Models.UsersModels;

namespace RealEstate.Application.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UsersResponseModel>> GetUsersAsync();
    Task<UsersResponseModel?> GetUserByIdAsync(int id, bool includeCompanyDetails);
    Task<List<UsersResponseModel>> GetUserByNameAsync(string userName, bool includeCompanyDetails);
    Task<UsersResponseModel> CreateUserAsync(CreateUsersRequestModel createUsersRequestModel);
    Task<UsersResponseModel> UpdateUserAsync(int id, CreateUsersRequestModel updateUsersRequestModel);
    void DeleteUserAsync(int id);

    Task<bool> SaveChangesAsync();
    Task<bool> isEmailUniqueAsync(string email);
}