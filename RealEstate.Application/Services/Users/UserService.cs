using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Models;
using RealEstate.DataAccess;
using RealEstate.DataAccess.Users;

namespace RealEstate.Application.Services.Users;

public class UserService : IUserService
{
    private readonly DatabaseContext _databaseContext;

//ToDo Repository - min 20:00
    public UserService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
    }

    public async Task<UsersResponseModel> CreateUserAsync(CreateUsersRequestModel createUsersRequestModel)
    {
        var user = createUsersRequestModel.ToUser();
        user.Role = createUsersRequestModel.isAgent ? Role.SalesAgent : Role.Customer;

        _databaseContext.Users.Add(user);
        await _databaseContext.SaveChangesAsync();

        return UsersResponseModel.FromUser(user);
    }

    public async Task<UsersResponseModel?> GetUserByIdAsync(int id, bool includeCompanyDetails)
    {
        if (includeCompanyDetails)
        {
            if (includeCompanyDetails && _databaseContext.Company == null)
                Console.WriteLine("There are no info about company");

            return await _databaseContext.Users.Where(c => c.Id == id).Include(c => c.Company)
                .Select(user => UsersResponseModel.FromUser(user))
                .FirstOrDefaultAsync();
        }

        return await _databaseContext.Users.Where(C => C.Id == id).Select(user => UsersResponseModel.FromUser(user))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<UsersResponseModel>> GetUsersAsync()
    {
        var usersFromDbQuery = _databaseContext.Users.Include(c => c.Company);
        var usersFromDb = await usersFromDbQuery.Select(user => UsersResponseModel.FromUser(user)).ToListAsync();

        return usersFromDb;
    }

    public async Task<UsersResponseModel> UpdateUserAsync(int id, CreateUsersRequestModel updateUsersRequestModel)
    {
        var userFromDb = await _databaseContext.Users.Where(x => x.Id == id).Include(x => x.Company).FirstAsync();
        if (userFromDb == null) throw new NullReferenceException("The user doesn't exist");

        _databaseContext.Attach(userFromDb);

        userFromDb.FirstName = updateUsersRequestModel.FirstName;
        userFromDb.LastName = updateUsersRequestModel.LastName;
        userFromDb.Email = updateUsersRequestModel.Email;
        userFromDb.Password = updateUsersRequestModel.Password;
        userFromDb.PhoneNumber = updateUsersRequestModel.PhoneNumber;
        userFromDb.Role = updateUsersRequestModel.isAgent ? Role.SalesAgent : Role.Customer;

        if (updateUsersRequestModel.isAgent)
        {
            userFromDb.Company.CompanyName = updateUsersRequestModel.Company.CompanyName;
            userFromDb.Company.CUI = updateUsersRequestModel.Company.CUI;
        }

        await _databaseContext.SaveChangesAsync();

        var updateUsersResponseModel = new UsersResponseModel
        {
            Id = userFromDb.Id,
            FirstName = userFromDb.FirstName,
            LastName = userFromDb.LastName,
            EmailAddress = userFromDb.Email,
            PhoneNumber = userFromDb.PhoneNumber,
            Role = userFromDb.Role
        };

        return updateUsersResponseModel;
    }

    public async void DeleteUserAsync(int id)
    {
        var userForDelete = _databaseContext.Users.Where(u => u.Id == id).First();
        if (userForDelete != null) _databaseContext.Users.Remove(userForDelete);
        _databaseContext.SaveChanges();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _databaseContext.SaveChangesAsync() >= 0;
    }


    public async Task<bool> isEmailUniqueAsync(string email)
    {
        return !_databaseContext.Users.Any(u => u.Email == email);
    }

    public async Task<List<UsersResponseModel>> GetUserByNameAsync(string userName, bool includeCompanyDetails)
    {
        var userNames = userName.Split(" ");

        var usersWithSameNameQuery = _databaseContext.Users.Include(c => c.Company);

        foreach (var userNAme in userNames)
            usersWithSameNameQuery.Where(x => x.FirstName == userNAme || x.LastName == userName);

        var usersWithSameName =
            await usersWithSameNameQuery.Select(user => UsersResponseModel.FromUser(user)).ToListAsync();

        return usersWithSameName;
    }
}