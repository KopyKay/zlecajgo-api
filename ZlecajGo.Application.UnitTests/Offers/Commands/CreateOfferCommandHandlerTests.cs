using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using ZlecajGo.Application.Offers.Commands.CreateOffer;
using ZlecajGo.Application.UnitTests.Helpers.TestData;
using ZlecajGo.Application.Users;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.UnitTests.Offers.Commands;

public class CreateOfferCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidRequest_ReturnsOfferId()
    {
        // Arrange
        var handler = CreateHandler(out var offerRepository, out var userContext, out var mapper);
        var request = OfferCommandTestData.CreateValidCreateOfferCommand();
        var user = new CurrentUser("provider-1");
        var mappedOffer = new Offer();
        var expectedId = Guid.NewGuid();

        userContext.GetCurrentUser().Returns(user);
        mapper.Map<Offer>(request).Returns(mappedOffer);
        offerRepository.CreateOfferAsync(mappedOffer).Returns(expectedId);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().Be(expectedId);
    }

    [Fact]
    public async Task Handle_UserContextProvidesId_OverridesProviderId()
    {
        // Arrange
        var handler = CreateHandler(out var offerRepository, out var userContext, out var mapper);
        var request = OfferCommandTestData.CreateValidCreateOfferCommand();
        request.ProviderId = "old-provider";

        userContext.GetCurrentUser().Returns(new CurrentUser("new-provider"));
        mapper.Map<Offer>(Arg.Any<CreateOfferCommand>()).Returns(new Offer());
        offerRepository.CreateOfferAsync(Arg.Any<Offer>()).Returns(Guid.NewGuid());

        // Act
        _ = await handler.Handle(request, CancellationToken.None);

        // Assert
        request.ProviderId.Should().Be("new-provider");
        mapper.Received(1).Map<Offer>(Arg.Is<CreateOfferCommand>(command => command.ProviderId == "new-provider"));
    }

    [Fact]
    public async Task Handle_MapsAndCallsRepository_CreatesOffer()
    {
        // Arrange
        var handler = CreateHandler(out var offerRepository, out var userContext, out var mapper);
        var request = OfferCommandTestData.CreateValidCreateOfferCommand();
        userContext.GetCurrentUser().Returns(new CurrentUser("provider-2"));

        var mappedOffer = new Offer();
        mapper.Map<Offer>(request).Returns(mappedOffer);
        offerRepository.CreateOfferAsync(mappedOffer).Returns(Guid.NewGuid());

        // Act
        _ = await handler.Handle(request, CancellationToken.None);

        // Assert
        mapper.Received(1).Map<Offer>(request);
        await offerRepository.Received(1).CreateOfferAsync(mappedOffer);
    }

    private static CreateOfferCommandHandler CreateHandler(
        out IOfferRepository offerRepository,
        out IUserContext userContext,
        out IMapper mapper)
    {
        var logger = Substitute.For<ILogger<CreateOfferCommandHandler>>();
        offerRepository = Substitute.For<IOfferRepository>();
        userContext = Substitute.For<IUserContext>();
        mapper = Substitute.For<IMapper>();

        return new CreateOfferCommandHandler(logger, offerRepository, userContext, mapper);
    }
}