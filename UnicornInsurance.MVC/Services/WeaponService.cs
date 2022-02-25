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

        public async Task<WeaponVM> GetWeaponDetails(int id)
        {
            var weapon = await _client.WeaponGETAsync(id);

            return _mapper.Map<WeaponVM>(weapon);
        }

        public async Task<List<WeaponVM>> GetWeapons()
        {
            var weapons = await _client.WeaponAllAsync();

            return _mapper.Map<List<WeaponVM>>(weapons);
        }

        public async Task<BaseCommandResponse> InsertWeapon(WeaponVM weaponVM)
        {
            var weapon = _mapper.Map<CreateWeaponDTO>(weaponVM);

            return await _client.WeaponPOSTAsync(weapon);
        }

        public async Task<BaseCommandResponse> UpdateWeapon(WeaponVM weaponVM)
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
