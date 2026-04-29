using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using ZlecajGo.Application.Offers.Commands.DeleteOffer;
using ZlecajGo.Application.UnitTests.Helpers.TestData;
using ZlecajGo.Application.Users;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.UnitTests.Offers.Commands;

public class DeleteOfferCommandHandlerTests
{
    [Fact]
    public async Task Handle_UserNotOwner_ThrowsNotAllowedException()
    {
        // Arrange
        var handler = CreateHandler(out var offerRepository, out var userContext);
        var offerId = Guid.NewGuid();
        var request = new DeleteOfferCommand(offerId);
        var offer = OfferTestData.Create(offerId, "owner");

        userContext.GetCurrentUser().Returns(new CurrentUser("not-owner"));
        offerRepository.GetOfferByIdAsync(offerId).Returns(offer);

        // Act
        var act = async () => await handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotAllowedException>();
    }

    [Fact]
    public async Task Handle_UserIsOwner_DeletesOfferAndReturnsTrue()
    {
        // Arrange
        var handler = CreateHandler(out var offerRepository, out var userContext);
        var offerId = Guid.NewGuid();
        var request = new DeleteOfferCommand(offerId);
        var offer = OfferTestData.Create(offerId, "owner");

        userContext.GetCurrentUser().Returns(new CurrentUser("owner"));
        offerRepository.GetOfferByIdAsync(offerId).Returns(offer);
        offerRepository.DeleteOfferAsync(offer).Returns(true);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        await offerRepository.Received(1).DeleteOfferAsync(offer);
    }

    private static DeleteOfferCommandHandler CreateHandler(
        out IOfferRepository offerRepository,
        out IUserContext userContext)
    {
        var logger = Substitute.For<ILogger<DeleteOfferCommandHandler>>();
        offerRepository = Substitute.For<IOfferRepository>();
        userContext = Substitute.For<IUserContext>();

        return new DeleteOfferCommandHandler(logger, offerRepository, userContext);
    }
}