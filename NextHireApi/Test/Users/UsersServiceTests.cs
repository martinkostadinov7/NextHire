using Business.Entities;
using Data.Abstraction;
using FluentAssertions;
using Moq;
using Service.Users;
using Shared.DTOs.Users;
using Xunit;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserService _service;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _service = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateUserAsync_ShouldCreateAndReturnUser()
    {
        // Arrange
        var dto = new UserCreateDto
        {
            Email = "ivan@test.com",
            FirstName = "Ivan",
            LastName = "Ivanov",
            Phone = "0888123456"
        };

        var savedUser = new User(dto.Email, dto.FirstName, dto.LastName, dto.Phone)
        {
            Id = 1,
            Applications = new List<Application>()
        };

        _userRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<User>()))
            .ReturnsAsync(savedUser);

        // Act
        var result = await _service.CreateUserAsync(dto, senderId: 1);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Email.Should().Be(dto.Email);
        _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task GetUserById_ShouldReturnUser()
    {
        // Arrange
        var user = new User("test@test.com", "Georgi", "Petrov", "0899")
        {
            Id = 10,
            Applications = new List<Application>()
        };

        _userRepositoryMock
            .Setup(r => r.GetByIdAsync(10))
            .ReturnsAsync(user);

        // Act
        var result = await _service.GetUserById(10);

        // Assert
        result.Should().NotBeNull();
        result.FirstName.Should().Be("Georgi");
    }

    [Fact]
    public async Task GetAllUsers_ShouldReturnList()
    {
        // Arrange
        var users = new List<User>
        {
            new User("u1@a.com", "N1", "L1", "1") { Id = 1 },
            new User("u2@a.com", "N2", "L2", "2") { Id = 2 }
        };

        _userRepositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(users);

        // Act
        var result = await _service.GetAllUsers();

        // Assert
        result.Should().HaveCount(2);
        result[0].Email.Should().Be("u1@a.com");
    }

    [Fact]
    public async Task UpdateUserAsync_ShouldUpdateExistingUser()
    {
        // Arrange
        var existing = new User("old@a.com", "Old", "Old", "000") { Id = 1 };
        var updateDto = new UserUpdateDto
        {
            Email = "new@a.com",
            FirstName = "New",
            LastName = "New",
            Phone = "111"
        };

        _userRepositoryMock
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(existing);

        _userRepositoryMock
            .Setup(r => r.UpdateAsync(existing))
            .ReturnsAsync(existing);

        // Act
        var result = await _service.UpdateUserAsync(1, updateDto);

        // Assert
        result.Email.Should().Be("new@a.com");
        result.FirstName.Should().Be("New");
        _userRepositoryMock.Verify(r => r.UpdateAsync(existing), Times.Once);
    }

    [Fact]
    public async Task UpdateUserAsync_ShouldThrow_WhenNotFound()
    {
        // Arrange
        _userRepositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((User)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() =>
            _service.UpdateUserAsync(99, new UserUpdateDto())
        );
    }

    [Fact]
    public async Task DeleteUserAsync_ShouldCallRepository()
    {
        // Act
        await _service.DeleteUserAsync(1);

        // Assert
        _userRepositoryMock.Verify(r => r.DeleteAsync(1), Times.Once);
    }
}