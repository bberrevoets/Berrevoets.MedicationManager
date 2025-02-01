using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Models;

namespace UserService.Services;

public class UserServiceImplementation : IUserService
{
    private readonly UserServiceDbContext _context;

    public UserServiceImplementation(UserServiceDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> GetUserAsync(int userId)
    {
        return await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task AssignRoleAsync(int userId, int roleId)
    {
        // You might add additional logic such as checking if the role exists
        var user = await _context.Users.FindAsync(userId);
        var role = await _context.Roles.FindAsync(roleId);

        if (user == null || role == null) throw new Exception("User or Role not found.");

        var userRole = new UserRole { UserId = userId, RoleId = roleId };
        _context.UserRoles.Add(userRole);
        await _context.SaveChangesAsync();
    }
}