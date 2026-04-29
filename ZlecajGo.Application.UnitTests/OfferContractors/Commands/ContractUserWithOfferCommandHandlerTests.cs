using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NSubstitute;
using ZlecajGo.Application.OfferContractors.Commands.ContractUserWithOffer;
using ZlecajGo.Application.Users;
using ZlecajGo.Domain.Constants;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Repositories;
using ZlecajGo.Application.UnitTests.Helpers.TestData;

namespace ZlecajGo.Application.UnitTests.OfferContractors.Commands;

public class ContractUserWithOfferCommandHandlerTests
{
    [Fact]
    public async Task Handle_AcceptedProposal_SetsPlannedStatus()
    {
        // Arrange
        var handler = CreateHandler(
            out var offerContractorRepository,
            out var userStore,
            out var offerRepository,
            out var userContext,
            out var mapper);

        var offerId = Guid.NewGuid();
        var contractorId = "contractor-1";
        var request = CreateRequest(offerId, contractorId);

        userContext.GetCurrentUser().Returns(new CurrentUser("provider-1"));
        userStore.FindByIdAsync(contractorId, Arg.Any<CancellationToken>())
            .Returns(UserTestData.Create(contractorId));
        offerRepository.GetOfferByIdAsync(offerId).Returns(OfferTestData.Create(offerId, "provider-1"));

        ContractUserWithOfferCommand? captured = null;
        mapper.Map<OfferContractor>(Arg.Do<ContractUserWithOfferCommand>(command => captured = command))
            .Returns(new OfferContractor());
        offerContractorRepository.ContractUserToOfferAsync(Arg.Any<OfferContractor>()).Returns(true);

        // Act
        _ = await handler.Handle(request, CancellationToken.None);

        // Assert
        captured.Should().NotBeNull();
        captured!.StatusId.Should().Be(AppStatuses.Planned.Id);
    }

    [Fact]
    public async Task Handle_RejectedProposal_ReturnsFalse()
    {
        // Arrange
        var handler = CreateHandler(
            out var offerContractorRepository,
            out var userStore,
            out var offerRepository,
            out var userContext,
            out var mapper);

        var offerId = Guid.NewGuid();
        var contractorId = "contractor-2";
        var request = CreateRequest(offerId, contractorId);

        userContext.GetCurrentUser().Returns(new CurrentUser("provider-2"));
        userStore.FindByIdAsync(contractorId, Arg.Any<CancellationToken>())
            .Returns(UserTestData.Create(contractorId));
        offerRepository.GetOfferByIdAsync(offerId).Returns(OfferTestData.Create(offerId, "provider-2"));
        mapper.Map<OfferContractor>(Arg.Any<ContractUserWithOfferCommand>()).Returns(new OfferContractor());
        offerContractorRepository.ContractUserToOfferAsync(Arg.Any<OfferContractor>()).Returns(false);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeFalse();
    }

    private static ContractUserWithOfferCommandHandler CreateHandler(
        out IOfferContractorRepository offerContractorRepository,
        out IUserStore<User> userStore,
        out IOfferRepository offerRepository,
        out IUserContext userContext,
        out IMapper mapper)
    {
        var logger = Substitute.For<ILogger<ContractUserWithOfferCommandHandler>>();
        offerContractorRepository = Substitute.For<IOfferContractorRepository>();
        userStore = Substitute.For<IUserStore<User>>();
        offerRepository = Substitute.For<IOfferRepository>();
        userContext = Substitute.For<IUserContext>();
        mapper = Substitute.For<IMapper>();

        return new ContractUserWithOfferCommandHandler(
            logger,
            offerContractorRepository,
            userStore,
            offerRepository,
            userContext,
            mapper);
    }

    private static ContractUserWithOfferCommand CreateRequest(Guid offerId, string contractorId) =>
        OfferContractorCommandTestData.CreateContractUserWithOfferCommand(offerId, contractorId);
}