using AutoMapper;
using ZlecajGo.Application.Offers.Commands.CreateOffer;
using ZlecajGo.Application.Offers.Commands.UpdateOfferStatus;
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
                opt.MapFrom(src => src.Location.Longitude));
        
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

        CreateMap<UpdateOfferCommand, Offer>()
            .ForMember(o => o.Description, opt =>
                opt.MapFrom((dto, og) => dto.Description ?? og.Description))
            .ForMember(o => o.Price, opt =>
                opt.MapFrom((dto, og) => dto.Price ?? og.Price))
            .ForMember(o => o.Images, opt =>
                opt.MapFrom((dto, og) => dto.Images ?? og.Images))
            .ForMember(o => o.Location, opt =>
                opt.MapFrom((dto, og) => new Location
                {
                    City = dto.City ?? og.Location.City,
                    Street = dto.Street ?? og.Location.Street,
                    ZipCode = dto.ZipCode ?? og.Location.ZipCode,
                    Latitude = dto.Latitude ?? og.Location.Latitude,
                    Longitude = dto.Longitude ?? og.Location.Longitude
                }))
            .ForMember(o => o.StatusId, opt =>
                opt.MapFrom((dto, og) => dto.StatusId ?? og.StatusId));
    }
}