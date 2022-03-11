using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Contracts;
using UnicornInsurance.MVC.Models;
using UnicornInsurance.MVC.Services.Base;

namespace UnicornInsurance.MVC.Services
{
    public class WeaponService : BaseHttpService, IWeaponService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IMapper _mapper;
        private readonly IClient _httpclient;

        public WeaponService(IMapper mapper, IClient httpclient, ILocalStorageService localStorageService) : base(httpclient, localStorageService)
        {
            _localStorageService = localStorageService;
            _mapper = mapper;
            _httpclient = httpclient;
        }        

        public async Task<Weapon> GetWeaponDetails(int id)
        {
            var weapon = await _client.WeaponGETAsync(id);

            return _mapper.Map<Weapon>(weapon);
        }

        public async Task<List<Weapon>> GetWeapons()
        {
            var weapons = await _client.WeaponAllAsync();

            return _mapper.Map<List<Weapon>>(weapons);
        }

        public async Task<BaseCommandResponse> InsertWeapon(Weapon weaponVM)
        {
            var weapon = _mapper.Map<CreateWeaponDTO>(weaponVM);

            return await _client.WeaponPOSTAsync(weapon);
        }

        public async Task<BaseCommandResponse> UpdateWeapon(Weapon weaponVM)
        {
            var weapon = _mapper.Map<WeaponDTO>(weaponVM);

            return await _client.WeaponPUTAsync(weapon);
        }

        public async Task DeleteWeapon(int id)
        {
            await _client.WeaponDELETEAsync(id);
        }
    }
}
