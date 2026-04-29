using ZlecajGo.Application.OfferContractors.Commands.ContractUserWithOffer;

namespace ZlecajGo.Application.UnitTests.Helpers.TestData;

public static class OfferContractorCommandTestData
{
    public static ContractUserWithOfferCommand CreateContractUserWithOfferCommand(Guid offerId, string contractorId) => new()
    {
        OfferId = offerId,
        ContractorId = contractorId,
        StartDateTime = DateTime.UtcNow.AddDays(1)
    };
}