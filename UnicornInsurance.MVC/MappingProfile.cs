using AutoMapper;
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

            CreateMap<MobileSuit, MobileSuitDTO>().ReverseMap();
            CreateMap<MobileSuit, FullMobileSuitDTO>().ReverseMap();
            CreateMap<MobileSuit, CreateFullMobileSuitDTO>().ReverseMap();

            CreateMap<Weapon, WeaponDTO>().ReverseMap();
            CreateMap<Weapon, FullWeaponDTO>().ReverseMap();
            CreateMap<Weapon, CreateWeaponDTO>().ReverseMap();

            CreateMap<MobileSuitCartItem, CreateMobileSuitCartItemDTO>().ReverseMap();
            CreateMap<MobileSuitCartItem, MobileSuitCartItemDTO>().ReverseMap();

            CreateMap<WeaponCartItem, CreateWeaponCartItemDTO>().ReverseMap();
            CreateMap<WeaponCartItem, WeaponCartItemDTO>().ReverseMap();

            CreateMap<OrderHeader, InitializeOrderHeaderDTO>().ReverseMap();
            CreateMap<OrderHeaderDTO, OrderHeader>()
                .ForMember(q => q.OrderDate , opt => opt.MapFrom(x => x.OrderDate.DateTime))
                .ForMember(q => q.PaymentDate, opt => opt.MapFrom(x => x.PaymentDate.DateTime))
                .ReverseMap();

            CreateMap<CompleteOrderHeader, CompleteOrderHeaderDTO>().ReverseMap();

            CreateMap<OrderDetailsVM, CreateOrderDetailsDTO>().ReverseMap();
            CreateMap<OrderDetailsVM, OrderDetailsDTO>().ReverseMap();

            CreateMap<MobileSuitCartItem, MobileSuitPurchaseDTO>().ReverseMap();
            CreateMap<MobileSuitCartItem, CreateMobileSuitPurchaseDTO>().ReverseMap();

            CreateMap<WeaponCartItem, WeaponPurchaseDTO>().ReverseMap();
            CreateMap<WeaponCartItem, CreateWeaponPurchaseDTO>().ReverseMap();

            CreateMap<UserMobileSuit, UserMobileSuitDTO>().ReverseMap();
            CreateMap<FullUserMobileSuitDTO, UserMobileSuit>()
                .ForMember(q => q.EndOfCoverage, opt => opt.MapFrom(x => x.EndOfCoverage.DateTime))
                .ReverseMap();

            CreateMap<UserWeapon, UserWeaponDTO>().ReverseMap();

            CreateMap<Deployment, CreateDeploymentDTO>().ReverseMap();
            CreateMap<Deployment, DeploymentDTO>().ReverseMap();
        }
    }
}
