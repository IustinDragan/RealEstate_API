using Microsoft.EntityFrameworkCore;
using RealEstate.API.DataAccess;
using RealEstate.API.DataAccess.Users;
using RealEstate.API.Models;

namespace RealEstate.API.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _databaseContext;

        public UserRepository(DatabaseContext databaseContext)
        {
            this._databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
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

            User users = new User
            {

                FirstName = updateUsersRequestModel.FirstName,
                LastName = updateUsersRequestModel.LastName,
                Email = updateUsersRequestModel.Email,
                Password = updateUsersRequestModel.Password,
                PhoneNumber = updateUsersRequestModel.PhoneNumber,
                Role = updateUsersRequestModel.isAgent ? Role.SalesAgent : Role.Customer,
                Company = !updateUsersRequestModel.isAgent ? null : new Company
                {
                    CompanyName = updateUsersRequestModel.Company.CompanyName,
                    CUI = updateUsersRequestModel.Company.CUI,
                }
            };

            CreateUsersResponseModel updateUsersResponseModel = new CreateUsersResponseModel
            {
                Id = id, //aici intoarce Id = 0.
                         //TODO: de investigat
                FirstName = updateUsersRequestModel.FirstName,
                LastName = updateUsersRequestModel.LastName,
                EmailAddress = updateUsersRequestModel.Email,
                PhoneNumber = updateUsersRequestModel.PhoneNumber,
                Role = users.Role
            };

            _databaseContext.Users.Update(users);
            await _databaseContext.SaveChangesAsync();

            return updateUsersResponseModel;
        }

        public async void DeleteUserAsync(int id)
        {
            var userForDelete = _databaseContext.Users.Where(u => u.Id == id).First();
            if(userForDelete != null)
            {
                _databaseContext.Users.Remove(userForDelete);
            }   
            _databaseContext.SaveChanges();
        }


        public async Task<bool> SaveChangesAsync()
        {
            return (await _databaseContext.SaveChangesAsync() >= 0);
        }


        public async Task<bool> isEmailUnique(string email)
        {
            return !_databaseContext.Users.Any(u => u.Email == email);
        }
    }
}
