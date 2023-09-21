using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RealEstate.API.DataAccess;
using RealEstate.API.DataAccess.Users;
using RealEstate.API.Models;

namespace RealEstate.Application.Services.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _databaseContext;

        public UserRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));

        }

        public async Task<CreateUsersResponseModel> CreateUserAsync(CreateUsersRequestModel createUsersRequestModel)
        {
            User users = new User
            {
                FirstName = createUsersRequestModel.FirstName,
                LastName = createUsersRequestModel.LastName,
                Email = createUsersRequestModel.Email,
                Password = createUsersRequestModel.Password,
                PhoneNumber = createUsersRequestModel.PhoneNumber,
                Role = createUsersRequestModel.isAgent ? Role.SalesAgent : Role.Customer,
                Company = !createUsersRequestModel.isAgent ? null : new Company
                {
                    CompanyName = createUsersRequestModel.Company.CompanyName,
                    CUI = createUsersRequestModel.Company.CUI,
                }
            };



            _databaseContext.Users.Add(users);
            await _databaseContext.SaveChangesAsync();

            CreateUsersResponseModel createUsersResponseModel = new CreateUsersResponseModel
            {
                Id = users.Id,
                FirstName = users.FirstName,
                LastName = users.LastName,
                EmailAddress = users.Email,
                PhoneNumber = users.PhoneNumber,
                Role = users.Role
            };

            return createUsersResponseModel;
        }

        //public async Task<IEnumerable<Company>> GetCompaniesForUserAsync(int userId)
        //{
        //    return await _databaseContext.Company.Where(c => c.Id == userId).ToListAsync();
        //}

        //public async Task<Company?> GetCompanyByIdAsync(int userId, int companyId)
        //{
        //    return await _databaseContext.Company.Where(c => c.Id == userId && c.Id == companyId).FirstOrDefaultAsync(); 
        //}

        public async Task<User?> GetUserByIdAsync(int cityId, bool includeCompanyDetails)
        {
            if (includeCompanyDetails)
            {
                if (includeCompanyDetails == true && _databaseContext.Company == null)
                {
                    Console.WriteLine("There are no info about company");
                }

                return await _databaseContext.Users.Include(c => c.Company).Where(c => c.Id == cityId).FirstOrDefaultAsync();
            }

            return await _databaseContext.Users.Where(C => C.Id == cityId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _databaseContext.Users.ToListAsync();
        }

        public async Task<CreateUsersResponseModel> UpdateUserAsync(int id, CreateUsersRequestModel updateUsersRequestModel)
        {
            var userFromDb = await _databaseContext.Users.Where(x => x.Id == id).FirstAsync();
            if (userFromDb == null)
            {
                throw new NullReferenceException("The user doesn't exist");
            }

            _databaseContext.Attach(userFromDb);

            userFromDb.FirstName = updateUsersRequestModel.FirstName;
            userFromDb.LastName = updateUsersRequestModel.LastName;
            userFromDb.Email = updateUsersRequestModel.Email;
            userFromDb.Password = updateUsersRequestModel.Password;
            userFromDb.PhoneNumber = updateUsersRequestModel.PhoneNumber;
            userFromDb.Role = updateUsersRequestModel.isAgent ? Role.SalesAgent : Role.Customer;

            if (updateUsersRequestModel.isAgent)
            {

                if (userFromDb.Company == null)
                {
                    userFromDb.Company = new Company();
                }
                userFromDb.Company.CompanyName = updateUsersRequestModel.Company.CompanyName;
                userFromDb.Company.CUI = updateUsersRequestModel.Company.CUI;
            }

            await _databaseContext.SaveChangesAsync();

            CreateUsersResponseModel updateUsersResponseModel = new CreateUsersResponseModel
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
            if (userForDelete != null)
            {
                _databaseContext.Users.Remove(userForDelete);
            }
            _databaseContext.SaveChanges();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _databaseContext.SaveChangesAsync() >= 0;
        }


        public async Task<bool> isEmailUnique(string email)
        {
            return !_databaseContext.Users.Any(u => u.Email == email);
        }

        public async Task<List<User?>> GetUserByNameAsync(string userName, bool includeCompanyDetails)
        {
            var usersWithSameName = await _databaseContext.Users.Include(c => c.Company).Where(c => c.FirstName == userName || c.LastName == userName).ToListAsync();

            if (includeCompanyDetails)
            {
                if (includeCompanyDetails == true && _databaseContext.Company == null)
                {
                    Console.WriteLine("There are no info about company");
                }
            }

            //if(_databaseContext.Users.Any(c=> (c.FirstName +" "+ c.LastName) == userName))
            //{
            //    return usersWithSameName;
            //}

            return usersWithSameName;
        }
    }
}
