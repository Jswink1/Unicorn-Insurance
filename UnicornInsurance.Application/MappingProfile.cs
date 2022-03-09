using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.MobileSuit;
using UnicornInsurance.Application.DTOs.MobileSuitCartItem;
using UnicornInsurance.Application.DTOs.OrderDetails;
using UnicornInsurance.Application.DTOs.OrderHeader;
using UnicornInsurance.Application.DTOs.UserMobileSuit;
using UnicornInsurance.Application.DTOs.UserWeapon;
using UnicornInsurance.Application.DTOs.Weapon;
using UnicornInsurance.Application.DTOs.WeaponCartItem;
using UnicornInsurance.Models;

namespace UnicornInsurance.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Weapon, WeaponDTO>().ReverseMap();
            CreateMap<Weapon, CreateWeaponDTO>().ReverseMap();
            CreateMap<Weapon, FullWeaponDTO>().ReverseMap();

            CreateMap<MobileSuit, MobileSuitDTO>().ReverseMap();
            CreateMap<MobileSuit, FullMobileSuitDTO>().ReverseMap();
            CreateMap<MobileSuit, CreateMobileSuitDTO>().ReverseMap();
            CreateMap<MobileSuit, CreateFullMobileSuitDTO>().ReverseMap();

            CreateMap<MobileSuitCartItem, CreateMobileSuitCartItemDTO>().ReverseMap();
            CreateMap<MobileSuitCartItem, MobileSuitCartItemDTO>().ReverseMap();

            CreateMap<WeaponCartItem, CreateWeaponCartItemDTO>().ReverseMap();
            CreateMap<WeaponCartItem, WeaponCartItemDTO>().ReverseMap();

            CreateMap<OrderHeader, InitializeOrderHeaderDTO>().ReverseMap();
            CreateMap<OrderHeader, OrderHeaderDTO>().ReverseMap();

            CreateMap<MobileSuitPurchase, MobileSuitPurchaseDTO>().ReverseMap();
            CreateMap<MobileSuitPurchase, CreateMobileSuitPurchaseDTO>().ReverseMap();

            CreateMap<WeaponPurchase, WeaponPurchaseDTO>().ReverseMap();
            CreateMap<WeaponPurchase, CreateWeaponPurchaseDTO>().ReverseMap();

            CreateMap<UserMobileSuit, UserMobileSuitDTO>().ReverseMap();
            CreateMap<UserMobileSuit, MobileSuitDTO>().ReverseMap();

            CreateMap<UserWeapon, UserWeaponDTO>().ReverseMap();
        }
    }
}
