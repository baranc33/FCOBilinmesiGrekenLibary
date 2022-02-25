using AutoMapper;
using FluentVal.DTOs;

namespace FluentVal.Models
{
    public class CoustomProfile : Profile
    {
        public CoustomProfile()
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(dto => dto.Isim, opt => opt.MapFrom(entity => entity.Name));

            CreateMap<Customer, CustomerDto>()
               .ForMember(dto => dto.EPosta, opt => opt.MapFrom(entity => entity.Email));

            CreateMap<Customer, CustomerDto>()
               .ForMember(dto => dto.Yas, opt => opt.MapFrom(entity => entity.Age)).ReverseMap();
            // reverse map yine aynı sekilde tersi işlem yapar.



            CreateMap<Customer, CustomerDto>()
               .ForMember(dto => dto.FullName, opt => opt.MapFrom(entity => entity.GetFullName()));

            CreateMap<CreditCard, CustomerDto>();
            
            CreateMap<Customer, CustomerDto>()
     .ForMember(dto => dto.Number, opt => opt.MapFrom(entity => entity.creditCard.Number));

        



        }
    }
}
