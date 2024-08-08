using AutoMapper;
using ZlecajGo.Application.Offers.Commands.CreateOffer;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Application.Offers.Dtos;

public class OfferProfile : Profile
{
    public OfferProfile()
    {
        CreateMap<Offer, OfferDto>()
            .ForMember(dto => dto.City, opt =>
                opt.MapFrom(src => src.Location.City))
            .ForMember(dto => dto.Street, opt =>
                opt.MapFrom(src => src.Location.Street))
            .ForMember(dto => dto.ZipCode, opt =>
                opt.MapFrom(src => src.Location.ZipCode))
            .ForMember(dto => dto.Latitude, opt =>
                opt.MapFrom(src => src.Location.Latitude))
            .ForMember(dto => dto.Longitude, opt =>
                opt.MapFrom(src => src.Location.Longitude))
            .ForMember(dto => dto.TypeName, opt =>
                opt.MapFrom(src => src.Type.Name))
            .ForMember(dto => dto.CategoryName, opt =>
                opt.MapFrom(src => src.Category.Name))
            .ForMember(dto => dto.StatusName, opt =>
                opt.MapFrom(src => src.Status.Name))
            .ForMember(dto => dto.ProviderFullName, opt =>
                opt.MapFrom(src => src.Provider.FullName));
        
        CreateMap<CreateOfferCommand, Offer>()
            .ForMember(o => o.Location, opt =>
                opt.MapFrom(dto => new Location
                {
                    City = dto.City,
                    Street = dto.Street,
                    ZipCode = dto.ZipCode,
                    Latitude = dto.Latitude,
                    Longitude = dto.Longitude
                }));
    }
}