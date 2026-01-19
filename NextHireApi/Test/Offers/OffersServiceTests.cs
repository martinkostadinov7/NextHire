using Business.Entities;
using Data.Abstraction;
using FluentAssertions;
using Moq;
using Service.Applications;
using Shared.DTOs.Offers;
using Xunit;

public class OfferServiceTests
{
    private readonly Mock<IOfferRepository> _offerRepositoryMock;
    private readonly OfferService _service;

    public OfferServiceTests()
    {
        _offerRepositoryMock = new Mock<IOfferRepository>();
        _service = new OfferService(_offerRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateOfferAsync_ShouldCreateAndReturnOffer()
    {
        // Arrange
        var dto = new OfferCreateDto
        {
            Title = "C# Developer",
            Description = "Looking for a junior dev",
            CompanyId = 1
        };

        var savedOffer = new Offer(dto.Title, dto.Description, dto.CompanyId)
        {
            Id = 1,
            Company = new Company("Tech Co", "Software"),
            Applications = new List<Application>()
        };

        _offerRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Offer>()))
            .ReturnsAsync(savedOffer);

        // Act
        var result = await _service.CreateOfferAsync(dto, senderId: 1);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be(dto.Title);
        result.Id.Should().Be(1);
        _offerRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Offer>()), Times.Once);
    }

    [Fact]
    public async Task GetOfferById_ShouldReturnOffer()
    {
        // Arrange
        var offer = new Offer("Frontend dev", "React expert", 2)
        {
            Id = 5,
            Company = new Company("Design Ltd", "UX/UI"),
            Applications = new List<Application>()
        };

        _offerRepositoryMock
            .Setup(r => r.GetByIdAsync(5))
            .ReturnsAsync(offer);

        // Act
        var result = await _service.GetOfferById(5);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be("Frontend dev");
    }

    [Fact]
    public async Task GetAllOffers_ShouldReturnList()
    {
        // Arrange
        var offers = new List<Offer>
        {
            new Offer("Dev 1", "Desc 1", 1) { Id = 1, Company = new Company("C1", "D1") },
            new Offer("Dev 2", "Desc 2", 2) { Id = 2, Company = new Company("C2", "D2") }
        };

        _offerRepositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(offers);

        // Act
        var result = await _service.GetAllOffers();

        // Assert
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task UpdateOfferAsync_ShouldUpdateExistingOffer()
    {
        // Arrange
        var existing = new Offer("Old Title", "Old Desc", 1) { Id = 1 };
        var updateDto = new OfferUpdateDto
        {
            Title = "New Title",
            Description = "New Desc",
            CompanyId = 2
        };

        _offerRepositoryMock
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(existing);

        _offerRepositoryMock
            .Setup(r => r.UpdateAsync(existing))
            .ReturnsAsync(existing);

        // Act
        var result = await _service.UpdateOfferAsync(1, updateDto);

        // Assert
        result.Title.Should().Be("New Title");
        _offerRepositoryMock.Verify(r => r.UpdateAsync(It.Is<Offer>(o => o.Title == "New Title")), Times.Once);
    }

    [Fact]
    public async Task UpdateOfferAsync_ShouldThrow_WhenNotFound()
    {
        // Arrange
        _offerRepositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Offer)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() =>
            _service.UpdateOfferAsync(99, new OfferUpdateDto())
        );
    }

    [Fact]
    public async Task DeleteOfferAsync_ShouldCallRepository()
    {
        // Act
        await _service.DeleteOfferAsync(1);

        // Assert
        _offerRepositoryMock.Verify(r => r.DeleteAsync(1), Times.Once);
    }
}