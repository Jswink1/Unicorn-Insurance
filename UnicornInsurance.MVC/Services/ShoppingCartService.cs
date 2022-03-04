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
    public class ShoppingCartService : BaseHttpService, IShoppingCartService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IMapper _mapper;
        private readonly IClient _httpclient;

        public ShoppingCartService(IMapper mapper, IClient httpclient, ILocalStorageService localStorageService) : base(httpclient, localStorageService)
        {
            _localStorageService = localStorageService;
            _mapper = mapper;
            _httpclient = httpclient;
        }        

        public async Task<BaseCommandResponse> AddMobileSuitCartItem(MobileSuitCartItem mobileSuitCartItem)
        {
            AddBearerToken();
            var mobileSuitCart = _mapper.Map<CreateMobileSuitCartItemDTO>(mobileSuitCartItem);

            var response = await _client.MobileSuitCartItemPOSTAsync(mobileSuitCart);

            return response;
        }

        public async Task<BaseCommandResponse> AddWeaponCartItem(WeaponCartItem weaponCartItem)
        {
            AddBearerToken();
            var weaponCart = _mapper.Map<CreateWeaponCartItemDTO>(weaponCartItem);

            var response = await _client.WeaponCartItemPOSTAsync(weaponCart);

            return response;
        }

        public async Task<List<MobileSuitCartItem>> GetAllMobileSuitCartItems()
        {
            AddBearerToken();
            var mobileSuitCartItems = await _client.MobileSuitCartItemAllAsync();

            return _mapper.Map<List<MobileSuitCartItem>>(mobileSuitCartItems);
        }

        public async Task<List<WeaponCartItem>> GetAllWeaponCartItems()
        {
            AddBearerToken();
            var weaponCartItems = await _client.WeaponCartItemAllAsync();

            return _mapper.Map<List<WeaponCartItem>>(weaponCartItems);
        }

        public async Task<ShoppingCartItemCountResponse> GetShoppingCartItemCount()
        {
            AddBearerToken();
            var response = await _client.ShoppingCartItemCountAsync();

            return response;
        }

        public async Task IncreaseWeaponQuantity(int itemId)
        {
            AddBearerToken();
            await _client.IncreaseWeaponQuantityAsync(itemId);
        }

        public async Task DecreaseWeaponQuantity(int itemId)
        {
            AddBearerToken();
            await _client.DecreaseWeaponQuantityAsync(itemId);
        }

        public async Task DeleteMobileSuitCartItem(int itemId)
        {
            AddBearerToken();
            await _client.MobileSuitCartItemDELETEAsync(itemId);
        }

        public async Task DeleteWeaponCartItem(int itemId)
        {
            AddBearerToken();
            await _client.WeaponCartItemDELETEAsync(itemId);
        }

        public async Task ClearShoppingCart()
        {
            AddBearerToken();
            await _client.ClearShoppingCartAsync();
        }
    }
}
