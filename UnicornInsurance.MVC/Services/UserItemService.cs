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
    public class UserItemService : BaseHttpService, IUserItemService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IMapper _mapper;
        private readonly IClient _httpclient;

        public UserItemService(IMapper mapper, IClient httpclient, ILocalStorageService localStorageService) : base(httpclient, localStorageService)
        {
            _localStorageService = localStorageService;
            _mapper = mapper;
            _httpclient = httpclient;
        }

        public async Task<List<UserMobileSuit>> GetAllUserMobileSuits()
        {
            AddBearerToken();
            var userMobileSuits = await _client.UserMobileSuitAllAsync();

            return _mapper.Map<List<UserMobileSuit>>(userMobileSuits);
        }

        public async Task<UserMobileSuit> GetUserMobileSuitDetails(int userMobileSuitId)
        {
            AddBearerToken();
            var userMobileSuit = await _client.UserMobileSuitAsync(userMobileSuitId);

            return _mapper.Map<UserMobileSuit>(userMobileSuit);
        }

        public async Task<BaseCommandResponse> EquipWeapon(int selectedWeaponId, int userMobileSuitId)
        {
            EquipWeaponDTO equipWeaponDTO = new()
            {
                SelectedWeaponId = selectedWeaponId,
                UserMobileSuitId = userMobileSuitId
            };

            AddBearerToken();
            var response = await _client.EquipWeaponAsync(equipWeaponDTO);

            return response;
        }

        public async Task UnequipWeapon(int userMobileSuitId)
        {
            AddBearerToken();
            await _client.UnequipWeaponAsync(userMobileSuitId);

            return;
        }

    }
}
