using MediatR;

namespace ZlecajGo.Application.Offers.Commands.DeleteOffer;

public record DeleteOfferCommand(Guid OfferId) : IRequest<bool>;