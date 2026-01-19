using Business.Entities;
using Data.Abstraction;
using FluentAssertions;
using Moq;
using Service.Applications;
using Shared.DTOs.Companies;
using Xunit;

public class CompanyServiceTests
{
    private readonly Mock<ICompanyRepository> _companyRepositoryMock;
    private readonly CompanyService _service;

    public CompanyServiceTests()
    {
        _companyRepositoryMock = new Mock<ICompanyRepository>();
        _service = new CompanyService(_companyRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateCompanyAsync_ShouldCreateAndReturnCompany()
    {
        // Arrange
        var dto = new CompanyCreateDto
        {
            Name = "Software House",
            Description = "Developing mobile apps"
        };

        var savedCompany = new Company(dto.Name, dto.Description)
        {
            Id = 1,
            Offers = new List<Offer>() // Празен списък, както е в обекта
        };

        _companyRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Company>()))
            .ReturnsAsync(savedCompany);

        // Act
        var result = await _service.CreateCompanyAsync(dto, senderId: 1);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Name.Should().Be(dto.Name);

        _companyRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Company>()), Times.Once);
    }

    [Fact]
    public async Task GetCompanyById_ShouldReturnCompany()
    {
        // Arrange
        var company = new Company("Tech Corp", "IT Solutions")
        {
            Id = 10
        };

        _companyRepositoryMock
            .Setup(r => r.GetByIdAsync(10))
            .ReturnsAsync(company);

        // Act
        var result = await _service.GetCompanyById(10);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Tech Corp");
    }

    [Fact]
    public async Task GetAllCompanys_ShouldReturnList()
    {
        // Arrange
        var companies = new List<Company>
        {
            new Company("Co 1", "Desc 1") { Id = 1 },
            new Company("Co 2", "Desc 2") { Id = 2 }
        };

        _companyRepositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(companies);

        // Act
        var result = await _service.GetAllCompanys();

        // Assert
        result.Should().HaveCount(2);
        result[0].Name.Should().Be("Co 1");
    }

    [Fact]
    public async Task UpdateCompanyAsync_ShouldUpdateExistingCompany()
    {
        // Arrange
        var existing = new Company("Old Name", "Old Desc") { Id = 1 };
        var updateDto = new CompanyUpdateDto
        {
            Name = "New Name",
            Description = "New Desc"
        };

        _companyRepositoryMock
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(existing);

        _companyRepositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<Company>()))
            .ReturnsAsync(existing);

        // Act
        var result = await _service.UpdateCompanyAsync(1, updateDto);

        // Assert
        result.Name.Should().Be("New Name");
        _companyRepositoryMock.Verify(r => r.UpdateAsync(existing), Times.Once);
    }

    [Fact]
    public async Task UpdateCompanyAsync_ShouldThrow_WhenNotFound()
    {
        // Arrange
        _companyRepositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Company)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() =>
            _service.UpdateCompanyAsync(99, new CompanyUpdateDto())
        );
    }

    [Fact]
    public async Task DeleteCompanyAsync_ShouldCallRepository()
    {
        // Arrange
        int testId = 5;

        // Act
        await _service.DeleteCompanyAsync(testId);

        // Assert
        _companyRepositoryMock.Verify(r => r.DeleteAsync(testId), Times.Once);
    }
}