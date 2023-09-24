using RealEstate.Application.Models;

namespace RealEstate.Application.Services.Users;

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

    //Task<IEnumerable<Company>> GetCompaniesForUserAsync(int userId);
    //Task<Company?> GetCompanyByIdAsync(int userId, int companyId);
}