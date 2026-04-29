using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using ZlecajGo.Application.Offers.Dtos;
using ZlecajGo.Application.Offers.Queries.GetOfferOrOffers;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Repositories;
using ZlecajGo.Application.UnitTests.Helpers.TestData;

namespace ZlecajGo.Application.UnitTests.Offers.Queries;

public class GetOfferOrOffersQueryHandlerTests
{
    [Fact]
    public async Task Handle_OffersExist_ReturnsMappedDtos()
    {
        // Arrange
        var handler = CreateHandler(out var offerRepository, out var mapper);

        var offers = CreateOffers();
        var mappedDtos = CreateMappedDtos(offers[0].Id, "provider");

        offerRepository.GetOffersAsync().Returns(offers);
        mapper.Map<IEnumerable<OfferDto>>(offers).Returns(mappedDtos);

        // Act
        var result = await handler.Handle(new GetOfferOrOffersQuery(null), CancellationToken.None);

        // Assert
        result.IsT1.Should().BeTrue();
        result.AsT1.Should().BeEquivalentTo(mappedDtos);
    }

    [Fact]
    public async Task Handle_NoOffers_ReturnsEmptyList()
    {
        // Arrange
        var handler = CreateHandler(out var offerRepository, out var mapper);

        var offers = Array.Empty<Offer>();
        var mappedDtos = Array.Empty<OfferDto>();

        offerRepository.GetOffersAsync().Returns(offers);
        mapper.Map<IEnumerable<OfferDto>>(offers).Returns(mappedDtos);

        // Act
        var result = await handler.Handle(new GetOfferOrOffersQuery(null), CancellationToken.None);

        // Assert
        result.IsT1.Should().BeTrue();
        result.AsT1.Should().BeEquivalentTo(mappedDtos);
    }

    private static GetOfferOrOffersQueryHandler CreateHandler(
        out IOfferRepository offerRepository,
        out IMapper mapper)
    {
        var logger = Substitute.For<ILogger<GetOfferOrOffersQueryHandler>>();
        offerRepository = Substitute.For<IOfferRepository>();
        mapper = Substitute.For<IMapper>();

        return new GetOfferOrOffersQueryHandler(logger, offerRepository, mapper);
    }


    private static List<Offer> CreateOffers() =>
        new()
        {
            OfferTestData.Create(providerId: "provider")
        };

    private static List<OfferDto> CreateMappedDtos(Guid offerId, string providerId) =>
        new()
        {
            new OfferDto { Id = offerId, ProviderId = providerId }
        };
}