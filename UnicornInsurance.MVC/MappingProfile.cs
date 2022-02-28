﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Models;
using UnicornInsurance.MVC.Services.Base;

namespace UnicornInsurance.MVC
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterVM, RegistrationRequest>().ReverseMap();

            CreateMap<MobileSuitVM, MobileSuitDTO>().ReverseMap();
            CreateMap<MobileSuitVM, FullMobileSuitDTO>().ReverseMap();
            CreateMap<MobileSuitVM, CreateFullMobileSuitDTO>().ReverseMap();

            CreateMap<WeaponVM, WeaponDTO>().ReverseMap();
            CreateMap<WeaponVM, FullWeaponDTO>().ReverseMap();
            CreateMap<WeaponVM, CreateWeaponDTO>().ReverseMap();

            CreateMap<MobileSuitCartItem, CreateMobileSuitCartItemDTO>().ReverseMap();
            CreateMap<MobileSuitCartItem, MobileSuitCartItemDTO>().ReverseMap();

            CreateMap<WeaponCartItem, CreateWeaponCartItemDTO>().ReverseMap();
            CreateMap<WeaponCartItem, WeaponCartItemDTO>().ReverseMap();

            CreateMap<OrderHeader, InitializeOrderHeaderDTO>().ReverseMap();

            CreateMap<OrderDetails, CreateOrderDetailsDTO>().ReverseMap();
            CreateMap<MobileSuitCartItem, MobileSuitPurchaseDTO>().ReverseMap();
            CreateMap<WeaponCartItem, WeaponPurchaseDTO>().ReverseMap();

            CreateMap<CompleteOrderHeader, CompleteOrderHeaderDTO>().ReverseMap();


        }
    }
}
