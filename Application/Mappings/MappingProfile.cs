using System;
using Application.DTOs;
using AutoMapper;
using Domain.Models;

namespace Application.Mappings
{
	public class MappingProfile : Profile
    {
		public MappingProfile()
		{
            CreateMap<PaymentTransaction, PaymentTransactionDto>()
             .ForMember(dto => dto.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod.ToString()))
             .ReverseMap()
             .ForMember(src => src.PaymentMethod, opt => opt.MapFrom(dto => Enum.Parse<PaymentMethodType>(dto.PaymentMethod)));

            CreateMap<PaymentStatus, PaymentStatusDto>().ReverseMap();


        }
    }
}

