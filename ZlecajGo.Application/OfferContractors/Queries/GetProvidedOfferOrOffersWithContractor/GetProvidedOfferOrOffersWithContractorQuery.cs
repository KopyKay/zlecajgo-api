using MediatR;
using OneOf;
using ZlecajGo.Application.OfferContractors.Dtos;

namespace ZlecajGo.Application.OfferContractors.Queries.GetProvidedOfferOrOffersWithContractor;

public record GetProvidedOfferOrOffersWithContractorQuery(string? ContractorId) : IRequest<OneOf<OfferContractorDto, IEnumerable<OfferContractorDto>>>;