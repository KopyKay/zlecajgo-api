using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using ZlecajGo.Application.Offers.Commands.UpdateOffer;
using ZlecajGo.Application.UnitTests.Helpers.TestData;
using ZlecajGo.Application.Users;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.UnitTests.Offers.Commands;

public class UpdateOfferCommandHandlerTests
{
    [Fact]
    public async Task Handle_OfferNotFound_ThrowsNotFoundException()
    {
        // Arrange
        var handler = CreateHandler(out var offerRepository, out var userContext, out _);
        var offerId = Guid.NewGuid();
        var request = new UpdateOfferCommand { OfferId = offerId };

        userContext.GetCurrentUser().Returns(new CurrentUser("provider-1"));
        offerRepository.GetOfferByIdWithTrackingAsync(offerId).Returns((Offer?)null);

        // Act
        var act = async () => await handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Handle_UserNotOwner_ThrowsNotAllowedException()
    {
        // Arrange
        var handler = CreateHandler(out var offerRepository, out var userContext, out _);
        var offerId = Guid.NewGuid();
        var request = new UpdateOfferCommand { OfferId = offerId };
        var offer = OfferTestData.Create(offerId, "owner");

        userContext.GetCurrentUser().Returns(new CurrentUser("not-owner"));
        offerRepository.GetOfferByIdWithTrackingAsync(offerId).Returns(offer);

        // Act
        var act = async () => await handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotAllowedException>();
    }

    [Fact]
    public async Task Handle_UserIsOwner_UpdatesOfferAndSaves()
    {
        // Arrange
        var handler = CreateHandler(out var offerRepository, out var userContext, out var mapper);
        var offerId = Guid.NewGuid();
        var request = new UpdateOfferCommand { OfferId = offerId, Description = "Updated" };
        var offer = OfferTestData.Create(offerId, "owner");

        userContext.GetCurrentUser().Returns(new CurrentUser("owner"));
        offerRepository.GetOfferByIdWithTrackingAsync(offerId).Returns(offer);

        // Act
        await handler.Handle(request, CancellationToken.None);

        // Assert
        mapper.Received(1).Map(request, offer);
        await offerRepository.Received(1).SaveChangesAsync();
    }

    private static UpdateOfferCommandHandler CreateHandler(
        out IOfferRepository offerRepository,
        out IUserContext userContext,
        out IMapper mapper)
    {
        var logger = Substitute.For<ILogger<UpdateOfferCommandHandler>>();
        offerRepository = Substitute.For<IOfferRepository>();
        userContext = Substitute.For<IUserContext>();
        mapper = Substitute.For<IMapper>();

        return new UpdateOfferCommandHandler(logger, offerRepository, userContext, mapper);
    }
}