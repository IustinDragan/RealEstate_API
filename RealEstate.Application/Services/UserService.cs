using RealEstate.Application.Exceptions;
using RealEstate.Application.Helpers;
using RealEstate.Application.Models.UsersModels;
using RealEstate.Application.Services.Interfaces;
using RealEstate.DataAccess.Enums;
using RealEstate.DataAccess.Repositories.Interfaces;
using RealEstate.Shared.Models.Users;

namespace RealEstate.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UsersResponseModel>> RealAllUsersAsync()
    {
        var users = await _userRepository.ReadAllAsync();

        return users.Select(UsersResponseModel.FromUser).ToList();
    }

    public async Task<UsersResponseModel?> GetUserByIdAsync(int id, bool includeCompanyDetails)
    {
        if (includeCompanyDetails)
            if (includeCompanyDetails && _userRepository.GetById(id).Result.Company == null)
                Console.WriteLine("There are no info about company");

        var userFromDb = await _userRepository.GetById(id);
        var fromUser = UsersResponseModel.FromUser(userFromDb);
        return fromUser;
    }

    public async Task<UsersResponseModel> GetUserByNameAsync(string userName, bool includeCompanyDetails)
    {
        if (includeCompanyDetails)
            if (includeCompanyDetails && _userRepository.GetByUsername(userName).Result.Company == null)
                Console.WriteLine("There are no info about company");

        var userFromDb = await _userRepository.GetByUsername(userName);

        var fromUser = UsersResponseModel.FromUser(userFromDb);
        return fromUser;
    }

    public async Task<UsersResponseModel> CreateUserAsync(CreateUsersRequestModel requestModel)
    {
        var user = requestModel.ToUser();
        user.Role = requestModel.isAgent ? Role.SalesAgent : Role.Customer;

        await _userRepository.AddAsync(user);

        return UsersResponseModel.FromUser(user);
    }

    public async Task<UsersResponseModel> UpdateUserAsync(int id, CreateUsersRequestModel requestModel)
    {
        var userFromDb = _userRepository.GetById(id);

        if (userFromDb == null) throw new NullReferenceException("The user doesn't exist");

        userFromDb.Result.FirstName = requestModel.FirstName;
        userFromDb.Result.LastName = requestModel.LastName;
        userFromDb.Result.Email = requestModel.Email;
        userFromDb.Result.Password = requestModel.Password;
        userFromDb.Result.PhoneNumber = requestModel.PhoneNumber;
        userFromDb.Result.Role = requestModel.isAgent ? Role.SalesAgent : Role.Customer;

        if (requestModel.isAgent)
        {
            userFromDb.Result.Company.CompanyName = requestModel.Company.CompanyName;
            userFromDb.Result.Company.CUI = requestModel.Company.CUI;
        }

        await _userRepository.UpdateAsync(id);

        var updateUsersResponseModel = new UsersResponseModel
        {
            Id = userFromDb.Id,
            FirstName = userFromDb.Result.FirstName,
            LastName = userFromDb.Result.LastName,
            EmailAddress = userFromDb.Result.Email,
            PhoneNumber = userFromDb.Result.PhoneNumber,
            Role = userFromDb.Result.Role
        };

        return updateUsersResponseModel;
    }

    public async Task DeleteUserAsync(int id)
    {
        await _userRepository.DeleteAsync(id);
    }

    public async Task<LoginResponseModel> LoginAsync(LoginRequestModel model)
    {
        var user = await _userRepository.GetByUsername(model.Username);

        if (user == null) throw new NotFoundException("Username or password is incorrect.");

        if (user.Password != model.Password) throw new NotFoundException("Username or password is incorrect.");

        var token = JwtHelper.GenerateToken(user, "MySuperSecretKey");

        return new LoginResponseModel
        {
            Username = user.UserName,
            Id = user.Id,
            Email = user.Email,
            Token = token
        };
    }

    public async Task<bool> isEmailUniqueAsync(string email)
    {
        var emailAddress = _userRepository.GetEmail(email).Result;

        if (emailAddress)
            return false;

        return true;
    }
}