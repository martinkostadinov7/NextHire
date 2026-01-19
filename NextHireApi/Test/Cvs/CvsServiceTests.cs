using Business.Entities;
using Data.Abstraction;
using FluentAssertions;
using Moq;
using Service.Applications;
using Shared.DTOs.Cvs;
using Xunit;

public class CvServiceTests
{
    private readonly Mock<ICvRepository> _cvRepositoryMock;
    private readonly CvService _service;

    public CvServiceTests()
    {
        _cvRepositoryMock = new Mock<ICvRepository>();
        _service = new CvService(_cvRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateCvAsync_ShouldCreateAndReturnCv()
    {
        // Arrange
        var dto = new CvCreateDto { UserId = 5 };
        var savedCv = new Cv(dto.UserId)
        {
            Id = 1,
            User = new User("test@test.com", "Ivan", "Ivanov", "0888")
        };

        _cvRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Cv>()))
            .ReturnsAsync(savedCv);

        // Act
        var result = await _service.CreateCvAsync(dto, senderId: 5);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        _cvRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Cv>()), Times.Once);
    }

    [Fact]
    public async Task GetCvById_ShouldReturnCv()
    {
        // Arrange
        var cv = new Cv(2)
        {
            Id = 10,
            User = new User("a@a.com", "User", "Test", "123")
        };

        _cvRepositoryMock
            .Setup(r => r.GetByIdAsync(10))
            .ReturnsAsync(cv);

        // Act
        var result = await _service.GetCvById(10);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(10);
    }

    [Fact]
    public async Task GetAllCvs_ShouldReturnList()
    {
        // Arrange
        var cvs = new List<Cv>
        {
            new Cv(1) { Id = 1, User = new User("u1@t.com", "N1", "L1", "1") },
            new Cv(2) { Id = 2, User = new User("u2@t.com", "N2", "L2", "2") }
        };

        _cvRepositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(cvs);

        // Act
        var result = await _service.GetAllCvs();

        // Assert
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task UpdateCvAsync_ShouldUpdateExistingCv()
    {
        // Arrange
        var existing = new Cv(1)
        {
            Id = 1,
            User = new User("old@test.com", "Old", "User", "111")
        };
        var updateDto = new CvUpdateDto { UserId = 10 };

        _cvRepositoryMock
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(existing);

        _cvRepositoryMock
            .Setup(r => r.UpdateAsync(existing))
            .ReturnsAsync(existing);

        // Act
        var result = await _service.UpdateCvAsync(1, updateDto);

        // Assert
        _cvRepositoryMock.Verify(r => r.UpdateAsync(It.Is<Cv>(c => c.UserId == 10)), Times.Once);
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateCvAsync_ShouldThrow_WhenNotFound()
    {
        // Arrange
        _cvRepositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Cv)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() =>
            _service.UpdateCvAsync(99, new CvUpdateDto())
        );
    }

    [Fact]
    public async Task DeleteCvAsync_ShouldCallRepository()
    {
        // Act
        await _service.DeleteCvAsync(1);

        // Assert
        _cvRepositoryMock.Verify(r => r.DeleteAsync(1), Times.Once);
    }
}