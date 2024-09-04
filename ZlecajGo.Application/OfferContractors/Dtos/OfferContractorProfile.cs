using AutoMapper;
using ZlecajGo.Application.OfferContractors.Commands.ContractUserWithOffer;
using ZlecajGo.Application.OfferContractors.Commands.UpdateContractedOffer;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Application.OfferContractors.Dtos;

public class OfferContractorProfile : Profile
{
    public OfferContractorProfile()
    {
        CreateMap<OfferContractor, OfferContractorDto>();
        CreateMap<ContractUserWithOfferCommand, OfferContractor>();
        CreateMap<UpdateContractedOfferCommand, OfferContractor>();
    }
}