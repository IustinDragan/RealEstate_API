using Microsoft.EntityFrameworkCore;
using RealEstate.DataAccess.Repositories.Interfaces;

namespace RealEstate.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _databaseContext;

    public UserRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<User> AddAsync(User user)
    {
        var addedEntity = await _databaseContext.Users.AddAsync(user);

        await _databaseContext.SaveChangesAsync();

        return addedEntity.Entity;
    }

    public async Task<User> UpdateAsync(int id)
    {
        var updatedEntity = await _databaseContext.Users.Where(x => x.Id == id).Include(x => x.Company).FirstAsync();

        await _databaseContext.SaveChangesAsync();

        return updatedEntity;
    }

    public async Task<List<User>> ReadAllAsync()
    {
        return await _databaseContext.Users.Include(c => c.Company).ToListAsync();
    }

    public async Task<bool> GetEmail(string email)
    {
        return _databaseContext.Users.Any(u => u.Email == email); //.Find(user.Email);
    }

    public async Task DeleteAsync(int id)
    {
        var userToDelete = await GetById(id);

        _databaseContext.Users.Remove(userToDelete);

        await _databaseContext.SaveChangesAsync();
    }

    public async Task<User?> GetById(int id)
    {
        return await _databaseContext.Users.Include(c => c.Company).Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<User?> GetByUsername(string username)
    {
        return await _databaseContext.Users.Include(c => c.Company)
            .Where(x => x.UserName == username)
            .FirstOrDefaultAsync();
        // return await _databaseContext.Users.Include(c => c.Company)
        //     .Where(x => x.FirstName == username || x.LastName == username)
        //     .FirstOrDefaultAsync();
    }
}