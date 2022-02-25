using AutoMapper;
using FluentVal.DTOs;

namespace FluentVal.Models
{
    public class EventDateProfile : Profile
    {
        public EventDateProfile()
        {
            CreateMap<EventDateDto, EventDate>()
                .ForMember(x => x.Date, opt => opt.MapFrom(y => new DateTime( y.Year, y.Mounth, y.Day )));
            // bu tarz complex işlemler için reverseMap(); metodu çalışmaz

            CreateMap<EventDate, EventDateDto>()
                .ForMember(x=> x.Year,option=> option.MapFrom(y=>y.Date.Year))
                .ForMember(x=> x.Mounth,option=> option.MapFrom(y=>y.Date.Month))
                .ForMember(x=> x.Day,option=> option.MapFrom(y=>y.Date.Day)));

        }
    }
}
