using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.MobileSuit;
using UnicornInsurance.Application.DTOs.MobileSuitCartItem;
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

            //CreateMap<CustomWeapon, WeaponDTO>().ReverseMap();




            //CreateMap<LeaveRequest, LeaveRequestListDTO>()
            //    .ForMember(dest => dest.DateRequested, opt => opt.MapFrom(src => src.DateCreated)).ReverseMap();

            //CreateMap<LeaveRequest, CreateLeaveRequestDTO>().ReverseMap();
            //CreateMap<LeaveRequest, UpdateLeaveRequestDTO>().ReverseMap();

            //CreateMap<LeaveAllocation, LeaveAllocationDTO>().ReverseMap();
            //CreateMap<LeaveAllocation, CreateLeaveRequestDTO>().ReverseMap();
            //CreateMap<LeaveAllocation, UpdateLeaveRequestDTO>().ReverseMap();

            //CreateMap<LeaveType, LeaveTypeDTO>().ReverseMap();
            //CreateMap<LeaveType, CreateLeaveTypeDTO>().ReverseMap();
        }
    }
}
