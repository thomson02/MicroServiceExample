using System;
using AutoMapper;
using MicroServiceExampleAPI.Data.Entities;
using MicroServiceExampleAPI.ViewModels;

namespace MicroServiceExampleAPI.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.Id))
                .ReverseMap();
        }
    }
}
