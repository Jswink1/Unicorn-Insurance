﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.MobileSuit;
using UnicornInsurance.Application.DTOs.UserWeapon;

namespace UnicornInsurance.Application.DTOs.UserMobileSuit
{
    public class FullUserMobileSuitDTO
    {
        public int Id { get; set; }
        public MobileSuitDTO MobileSuit { get; set; }
        public UserWeaponDTO EquippedWeapon { get; set; }
        public List<UserWeaponDTO> AvailableWeapons { get; set; }
    }
}
