using AutoMapper;
using CarDealership.Models;
using CarDealership.DTOs;

namespace CarDealership.Helpers
{
    public static class MappingExtensions
    {
        // When we're mapping from DTOs to Entities, we want to not map the Id property. Id's should only be set explicitly by the server.
        public static IMappingExpression<I, O> IgnoreId<I, O>(this IMappingExpression<I, O> expr) where O : ModelBaseWithGuid
        {
            return expr.ForMember(e => e.Id, m => m.MapFrom((dto, ent) => ent.Id));
        }
    }

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Address, AddressDTO>();
            CreateMap<AddressDTO, Address>();

            CreateMap<Car, CarDTO>()
                .ForMember(dto => dto.BodyStyleName, m => m.MapFrom(entity => entity.BodyStyle.GetDisplayName()))
                .ForMember(dto => dto.TransmissionTypeName, m => m.MapFrom(entity => entity.TransmissionType.GetDisplayName()));
            CreateMap<CarDTO, Car>();

            // Special handling for each lookup type to get the enum display name instead of the internal name
            CreateMap<BodyStyle, LookupDTO>()
                .ForMember(dto => dto.Name, m => m.MapFrom(entity => ((BodyStyles) entity.Id).GetDisplayName()));
            CreateMap<LookupDTO, BodyStyle>();

            CreateMap<TransmissionType, LookupDTO>()
                .ForMember(dto => dto.Name, m => m.MapFrom(entity => ((TransmissionTypes) entity.Id).GetDisplayName()));
            CreateMap<LookupDTO, TransmissionType>();
        }
    }
}
