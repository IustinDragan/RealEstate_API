using RealEstate.DataAccess.Entities;

namespace RealEstate.DataAccess.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> AddAsync(User user);

    Task<User> UpdateAsync(int id);

    Task<List<User>> ReadAllAsync();

    Task<User?> GetById(int id);

    Task<bool> GetEmail(string email);

    Task DeleteAsync(int id);

    Task<User?> GetByUsername(string username);

    Task<UserAnnouncement?> GetFavoriteAnnouncementAsync(int userId, int announcementId);

    Task AddFavoriteAnnouncementAsync(UserAnnouncement userAnnouncement);
}