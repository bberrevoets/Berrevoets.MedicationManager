using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Models;
using UserService.Services;

namespace UserService.Tests;

public class UserServiceTests
{
    private UserServiceDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<UserServiceDbContext>()
            .UseInMemoryDatabase("UserServiceTestDb")
            .Options;
        return new UserServiceDbContext(options);
    }

    [Fact]
    public async Task CreateUserAsync_ShouldReturnCreatedUser()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var service = new UserServiceImplementation(context);
        var newUser = new User { UserName = "testuser" };

        // Act
        var createdUser = await service.CreateUserAsync(newUser);

        // Assert
        Assert.NotNull(createdUser);
        Assert.Equal("testuser", createdUser.UserName);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllUsers()
    {
        // Arrange
        var context = GetInMemoryDbContext();

        // Add multiple users to the context
        context.Users.AddRange(
            new User { UserName = "user1" },
            new User { UserName = "user2" },
            new User { UserName = "user3" }
        );
        await context.SaveChangesAsync();

        // Create the service with the seeded context
        var service = new UserServiceImplementation(context);

        // Act
        var users = await service.GetAllAsync();

        // Assert
        Assert.NotNull(users);
        var userList = users.ToList();

        Assert.Equal(3, userList.Count);
        Assert.Contains(userList, u => u.UserName == "user1");
        Assert.Contains(userList, u => u.UserName == "user2");
        Assert.Contains(userList, u => u.UserName == "user3");
    }
}