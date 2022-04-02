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
    public class OrderService : BaseHttpService, IOrderService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IMapper _mapper;
        private readonly IClient _httpclient;

        public OrderService(IMapper mapper, IClient httpclient, ILocalStorageService localStorageService) : base(httpclient, localStorageService)
        {
            _localStorageService = localStorageService;
            _mapper = mapper;
            _httpclient = httpclient;
        }

        public async Task<List<OrderHeader>> GetOrders()
        {
            AddBearerToken();
            var ordersListDTO = await _client.OrdersAsync();
                
            return _mapper.Map<List<OrderHeader>>(ordersListDTO);
        }

        public async Task<OrderDetailsVM> GetOrderDetails(int orderId)
        {
            AddBearerToken();
            var orderDTO = await _client.OrderDetailsAsync(orderId);

            return _mapper.Map<OrderDetailsVM>(orderDTO);
        }

        public async Task<OrderHeader> GetOrderHeader(int orderId)
        {
            AddBearerToken();
            var orderheaderDTO = await _client.OrderHeaderAsync(orderId);

            return _mapper.Map<OrderHeader>(orderheaderDTO);
        }

        public async Task<BaseCommandResponse> InitializeOrder(ShoppingCartVM shoppingCart)
        {
            AddBearerToken();
            InitializeOrderDTO initializeOrderDTO = new()
            {
                MobileSuitPurchases = new List<CreateMobileSuitPurchaseDTO>(),
                WeaponPurchases = new List<CreateWeaponPurchaseDTO>()
            };

            foreach (var mobileSuitCartItem in shoppingCart.MobileSuitCartItems)            
                initializeOrderDTO.MobileSuitPurchases.Add(new CreateMobileSuitPurchaseDTO() { MobileSuitId = mobileSuitCartItem.MobileSuit.Id });

            foreach (var weaponCartItem in shoppingCart.WeaponCartItems)
                initializeOrderDTO.WeaponPurchases.Add(new CreateWeaponPurchaseDTO() { WeaponId = weaponCartItem.Weapon.Id, Count = weaponCartItem.Count });            

            return await _client.InitializeOrderAsync(initializeOrderDTO);
        }

        public async Task<BaseCommandResponse> CompleteOrder(CompleteOrderHeader orderHeaderCompletion)
        {
            AddBearerToken();
            var orderHeaderCompletionDTO = _mapper.Map<CompleteOrderDTO>(orderHeaderCompletion);

            return await _client.CompleteOrderAsync(orderHeaderCompletionDTO);
        }
    }
}
