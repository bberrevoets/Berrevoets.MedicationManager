using UserService.Models;

namespace UserService.Services;

public interface IUserService
{
    Task<User> CreateUserAsync(User user);
    Task<User> GetUserAsync(int userId);
    Task<IEnumerable<User>> GetAllAsync();
    Task AssignRoleAsync(int userId, int roleId);
}