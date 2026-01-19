using Business.Entities;
using Data.Abstraction;
using FluentAssertions;
using Moq;
using NextHireApi.Service.Applications;
using Shared.DTOs.Applications;
using Xunit;

public class ApplicationServiceTests
{
    private readonly Mock<IApplicationRepository> _applicationRepositoryMock;
    private readonly ApplicationService _service;

    public ApplicationServiceTests()
    {
        _applicationRepositoryMock = new Mock<IApplicationRepository>();
        _service = new ApplicationService(_applicationRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateApplicationAsync_ShouldCreateAndReturnApplication()
    {
        var dto = new ApplicationCreateDto
        {
            Email = "test@test.com",
            FirstName = "Ivan",
            LastName = "Ivanov",
            Phone = "0888123456",
            Education = "Bachelor",
            CoverLetter = "Cover",
            CvId = 1,
            UserId = 2,
            OfferId = 3
        };

        var savedApplication = new Application(
            dto.Email, dto.FirstName, dto.LastName,
            dto.Phone, dto.Education, dto.CoverLetter,
            dto.CvId, dto.UserId, dto.OfferId
        )
        {
            Id = 1, // Задай конкретно Id
            Cv = new Cv(dto.UserId),
            User = new User("user@test.com", "Ivan", "Ivanov", "0888"),
            Offer = new Offer("Title", "Description", 1),
            SubmittedAt = DateTime.Now
        };


        _applicationRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Application>()))
            .ReturnsAsync(savedApplication);

        var result = await _service.CreateApplicationAsync(dto, senderId: 1);

        result.Should().NotBeNull();
        result.Email.Should().Be(dto.Email);

        _applicationRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Application>()), Times.Once);
    }


    [Fact]
    public async Task GetApplicationById_ShouldReturnApplication()
    {
        var application = new Application(
            "a@a.com", "Ivan", "Ivanov",
            "0888", "Bachelor", "Cover",
            1, 2, 3
        )
        {
            Cv = new Cv(2),
            User = new User("a@a.com", "Ivan", "Ivanov", "0888"),
            Offer = new Offer("Title", "Desc", 1)
        };


        _applicationRepositoryMock
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(application);

        var result = await _service.GetApplicationById(1);

        result.Should().NotBeNull();
        result.Email.Should().Be("a@a.com");
    }

    [Fact]
    public async Task GetAllApplications_ShouldReturnList()
    {
        var applications = new List<Application>
        {
            new Application(
                "a@a.com", "Ivan", "Ivanov",
                "0888", "Bachelor", "Cover",
                1, 2, 3
            )
            {
                Cv = new Cv(2),
                User = new User("a@a.com", "Ivan", "Ivanov", "0888"),
                Offer = new Offer("Title", "Desc", 1)
            },

            new Application(
                "a@a.com", "Ivan", "Ivanov",
                "0888", "Bachelor", "Cover",
                1, 2, 3
            )
            {
                Cv = new Cv(2),
                User = new User("a@a.com", "Ivan", "Ivanov", "0888"),
                Offer = new Offer("Title", "Desc", 1)
            }
        };

        _applicationRepositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(applications);

        var result = await _service.GetAllApplications();

        result.Should().HaveCount(2);
        result[0].Email.Should().Be("a@a.com");
    }

    [Fact]
    public async Task UpdateApplicationAsync_ShouldUpdateExistingApplication()
    {
        var existing = new Application(
            "a@a.com", "Ivan", "Ivanov",
            "0888", "Bachelor", "Cover",
            1, 2, 3
        )
        {
            Cv = new Cv(2),
            User = new User("a@a.com", "Ivan", "Ivanov", "0888"),
            Offer = new Offer("Title", "Desc", 1)
        };

        var updateDto = new ApplicationUpdateDto
        {
            Email = "new@new.com",
            FirstName = "New",
            LastName = "Name",
            Phone = "1111",
            Education = "NewEdu",
            CoverLetter = "NewCover",
            CvId = 2,
            UserId = 3,
            OfferId = 4
        };

        _applicationRepositoryMock
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(existing);

        _applicationRepositoryMock
            .Setup(r => r.UpdateAsync(existing))
            .ReturnsAsync(existing);

        var result = await _service.UpdateApplicationAsync(1, updateDto);

        result.Email.Should().Be("new@new.com");
        result.FirstName.Should().Be("New");

        _applicationRepositoryMock.Verify(r => r.GetByIdAsync(1), Times.Once);
        _applicationRepositoryMock.Verify(r => r.UpdateAsync(existing), Times.Once);
    }

    [Fact]
    public async Task UpdateApplicationAsync_ShouldThrow_WhenNotFound()
    {
        _applicationRepositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Application)null);

        await Assert.ThrowsAsync<Exception>(() =>
            _service.UpdateApplicationAsync(1, new ApplicationUpdateDto())
        );
    }

    [Fact]
    public async Task DeleteApplicationAsync_ShouldCallRepository()
    {
        _applicationRepositoryMock
            .Setup(r => r.DeleteAsync(1))
            .ReturnsAsync(true);

        await _service.DeleteApplicationAsync(1);

        _applicationRepositoryMock.Verify(r => r.DeleteAsync(1), Times.Once);
    }


}
