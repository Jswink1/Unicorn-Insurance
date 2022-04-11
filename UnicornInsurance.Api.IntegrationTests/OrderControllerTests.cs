using Newtonsoft.Json;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.Order;
using UnicornInsurance.Application.DTOs.OrderDetails;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Api.IntegrationTests
{
    public class OrderControllerTests : IntegrationTest
    {
        [Test]
        [Order(1)]
        public async Task InitializeAndCompleteOrder()
        {
            await AuthenticateUserAsync();

            InitializeOrderDTO initializeOrderDTO = new()
            {
                MobileSuitPurchases = new()
                {
                    new CreateMobileSuitPurchaseDTO()
                    {
                        MobileSuitId = 2                        
                    }
                },
                WeaponPurchases = new()
                {
                    new CreateWeaponPurchaseDTO()
                    {
                        WeaponId = 1,
                        Count = 1
                    }
                }
            };

            var response = await TestClient.PostAsJsonAsync("https://localhost:5001/api/InitializeOrder", initializeOrderDTO);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            var baseCommandResponse = JsonConvert.DeserializeObject<BaseCommandResponse>(jsonString);

            baseCommandResponse.Success.ShouldBeTrue();

            CompleteOrderDTO completeOrderDTO = new()
            {
                OrderId = baseCommandResponse.Id,
                TransactionSuccess = true,
                TransactionId = "12345"
            };

            response = await TestClient.PostAsJsonAsync("https://localhost:5001/api/CompleteOrder", completeOrderDTO);

            jsonString = await response.Content.ReadAsStringAsync();
            baseCommandResponse = JsonConvert.DeserializeObject<BaseCommandResponse>(jsonString);

            baseCommandResponse.Success.ShouldBeTrue();
        }

        [Test]
        [Order(2)]
        public async Task GetOrders()
        {
            await AuthenticateUserAsync();

            var response = await TestClient.GetAsync("https://localhost:5001/api/Orders");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            var orders = JsonConvert.DeserializeObject<List<OrderHeaderDTO>>(jsonString);

            orders.Count.ShouldBe(1);

            foreach (var order in orders)
            {
                response = await TestClient.GetAsync($"https://localhost:5001/api/OrderDetails/{order.Id}");

                response.StatusCode.ShouldBe(HttpStatusCode.OK);

                response = await TestClient.GetAsync($"https://localhost:5001/api/OrderHeader/{order.Id}");

                response.StatusCode.ShouldBe(HttpStatusCode.OK);
            }
        }       
    }
}
