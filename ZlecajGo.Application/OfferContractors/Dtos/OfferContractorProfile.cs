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
        
        CreateMap<ContractUserWithOfferCommand, OfferContractor>()
            .ForMember(o => o.StartDateTime, opt =>
                opt.MapFrom(dto => dto.StartDateTime.Kind != DateTimeKind.Utc
                    ? dto.StartDateTime.ToUniversalTime()
                    : dto.StartDateTime));
                    
        CreateMap<UpdateContractedOfferCommand, OfferContractor>()
            .ForMember(o => o.StartDateTime, opt =>
                opt.MapFrom(dto => dto.StartDateTime.Kind != DateTimeKind.Utc
                    ? dto.StartDateTime.ToUniversalTime()
                    : dto.StartDateTime));
    }
}