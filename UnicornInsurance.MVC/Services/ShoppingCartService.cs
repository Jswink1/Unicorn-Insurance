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

        public async Task<BaseCommandResponse> AddMobileSuitCartItem(int mobileSuitId)
        {
            AddBearerToken();
            var response = await _client.AddMobileSuitToCartAsync(mobileSuitId);

            return response;
        }

        public async Task<BaseCommandResponse> AddWeaponCartItem(int weaponId)
        {
            AddBearerToken();
            var response = await _client.AddWeaponToCartAsync(weaponId);

            return response;
        }

        public async Task<List<MobileSuitCartItem>> GetAllMobileSuitCartItems()
        {
            AddBearerToken();
            var mobileSuitCartItems = await _client.MobileSuitCartItemsListAsync();

            return _mapper.Map<List<MobileSuitCartItem>>(mobileSuitCartItems);
        }

        public async Task<List<WeaponCartItem>> GetAllWeaponCartItems()
        {
            AddBearerToken();
            var weaponCartItems = await _client.WeaponCartItemsListAsync();

            return _mapper.Map<List<WeaponCartItem>>(weaponCartItems);
        }

        public async Task<ShoppingCartItemCountResponse> GetShoppingCartItemCount()
        {
            AddBearerToken();
            var response = await _client.ShoppingCartItemCountAsync();

            return response;
        }

        public async Task IncreaseWeaponQuantity(int weaponId)
        {
            AddBearerToken();
            await _client.WeaponCartItemQuantityIncreaseAsync(weaponId);
        }

        public async Task DecreaseWeaponQuantity(int weaponId)
        {
            AddBearerToken();
            await _client.WeaponCartItemQuantityDecreaseAsync(weaponId);
        }

        public async Task DeleteMobileSuitCartItem(int cartItemId)
        {
            AddBearerToken();
            await _client.DeleteMobileSuitCartItemAsync(cartItemId);
        }

        public async Task DeleteWeaponCartItem(int cartItemId)
        {
            AddBearerToken();
            await _client.DeleteWeaponCartItemAsync(cartItemId);
        }

        public async Task ClearShoppingCart()
        {
            AddBearerToken();
            await _client.ClearShoppingCartAsync();
        }
    }
}
