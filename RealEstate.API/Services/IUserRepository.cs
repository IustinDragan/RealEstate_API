using Microsoft.AspNetCore.Mvc;
using RealEstate.API.DataAccess.Users;
using RealEstate.API.Models;

namespace RealEstate.API.Services
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetUserByIdAsync(int cityId, bool includeCompanyDetails);
        //Task<User?> GetUserByNameAsync(string userName);
        Task<CreateUsersResponseModel> CreateUserAsync(CreateUsersRequestModel createUsersRequestModel);
        Task<CreateUsersResponseModel> UpdateUserAsync(int id, CreateUsersRequestModel updateUsersRequestModel);
        void DeleteUserAsync(int id);


        Task<bool> SaveChangesAsync();
        Task<bool> isEmailUnique(string email);

        //Task<IEnumerable<Company>> GetCompaniesForUserAsync(int userId);
        //Task<Company?> GetCompanyByIdAsync(int userId, int companyId);
    }
}
