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
using UnicornInsurance.Application.DTOs.MobileSuitCartItem;
using UnicornInsurance.Application.DTOs.WeaponCartItem;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Api.IntegrationTests
{
    public class ShoppingCartControllerTests : IntegrationTest
    {
        [Test]
        [Order(1)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task AddWeaponToCart(int weaponId)
        {
            await AuthenticateUserAsync();

            var weaponCartItemsResponse = await TestClient.GetAsync("https://localhost:5001/api/WeaponCartItemsList");
            var weaponCartItems = JsonConvert.DeserializeObject<List<WeaponCartItemDTO>>(await weaponCartItemsResponse.Content.ReadAsStringAsync());
            var count = weaponCartItems.Count;

            var response = await TestClient.PostAsJsonAsync("https://localhost:5001/api/AddWeaponToCart/", weaponId);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            var baseCommandResponse = JsonConvert.DeserializeObject<BaseCommandResponse>(jsonString);

            baseCommandResponse.Success.ShouldBeTrue();

            weaponCartItemsResponse = await TestClient.GetAsync("https://localhost:5001/api/WeaponCartItemsList");
            weaponCartItems = JsonConvert.DeserializeObject<List<WeaponCartItemDTO>>(await weaponCartItemsResponse.Content.ReadAsStringAsync());
            weaponCartItems.Count.ShouldBe(count + 1);
        }

        [Test]
        [Order(2)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task AddMobileSuitToCart(int mobileSuitId)
        {
            await AuthenticateUserAsync();

            var mobileSuitCartItemsResponse = await TestClient.GetAsync("https://localhost:5001/api/MobileSuitCartItemsList");
            var mobileSuitCartItems = JsonConvert.DeserializeObject<List<MobileSuitCartItemDTO>>(await mobileSuitCartItemsResponse.Content.ReadAsStringAsync());
            var count = mobileSuitCartItems.Count;

            var response = await TestClient.PostAsJsonAsync("https://localhost:5001/api/AddMobileSuitToCart/", mobileSuitId);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            var baseCommandResponse = JsonConvert.DeserializeObject<BaseCommandResponse>(jsonString);

            baseCommandResponse.Success.ShouldBeTrue();

            mobileSuitCartItemsResponse = await TestClient.GetAsync("https://localhost:5001/api/MobileSuitCartItemsList");
            mobileSuitCartItems = JsonConvert.DeserializeObject<List<MobileSuitCartItemDTO>>(await mobileSuitCartItemsResponse.Content.ReadAsStringAsync());
            mobileSuitCartItems.Count.ShouldBe(count + 1);
        }

        [Test]
        [Order(3)]
        public async Task GetWeaponCartItems()
        {
            await AuthenticateUserAsync();

            var response = await TestClient.GetAsync("https://localhost:5001/api/WeaponCartItemsList");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            var weaponCartItems = JsonConvert.DeserializeObject<List<WeaponCartItemDTO>>(jsonString);

            weaponCartItems.Count.ShouldBe(3);
        }

        [Test]
        [Order(4)]
        public async Task GetMobileSuitCartItems()
        {
            await AuthenticateUserAsync();

            var response = await TestClient.GetAsync("https://localhost:5001/api/MobileSuitCartItemsList");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            var mobileSuitCartItems = JsonConvert.DeserializeObject<List<MobileSuitCartItemDTO>>(jsonString);

            mobileSuitCartItems.Count.ShouldBe(3);
        }

        [Test]
        [Order(5)]
        public async Task GetShoppingCartItemCount()
        {
            await AuthenticateUserAsync();

            var response = await TestClient.GetAsync("https://localhost:5001/api/ShoppingCartItemCount");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            var itemCountResponse = JsonConvert.DeserializeObject<ShoppingCartItemCountResponse>(jsonString);

            itemCountResponse.ShoppingCartItemCount.ShouldBe(6);
        }

        [Test]
        [Order(6)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task IncreaseWeaponQuantity(int weaponId)
        {
            await AuthenticateUserAsync();

            var response = await TestClient.PutAsJsonAsync("https://localhost:5001/api/WeaponCartItemQuantityIncrease/", weaponId);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            var baseCommandResponse = JsonConvert.DeserializeObject<BaseCommandResponse>(jsonString);

            baseCommandResponse.Success.ShouldBeTrue();

            var weaponCartItemsResponse = await TestClient.GetAsync("https://localhost:5001/api/WeaponCartItemsList");
            var weaponCartItems = JsonConvert.DeserializeObject<List<WeaponCartItemDTO>>(await weaponCartItemsResponse.Content.ReadAsStringAsync());
            var weaponCartItem = weaponCartItems.Where(i => i.Weapon.Id == weaponId).FirstOrDefault();

            weaponCartItem.Count.ShouldBe(2);
        }

        [Test]
        [Order(7)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task DecreaseWeaponQuantity(int weaponId)
        {
            await AuthenticateUserAsync();

            var response = await TestClient.PutAsJsonAsync("https://localhost:5001/api/WeaponCartItemQuantityDecrease/", weaponId);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            var baseCommandResponse = JsonConvert.DeserializeObject<BaseCommandResponse>(jsonString);

            baseCommandResponse.Success.ShouldBeTrue();

            var weaponCartItemsResponse = await TestClient.GetAsync("https://localhost:5001/api/WeaponCartItemsList");
            var weaponCartItems = JsonConvert.DeserializeObject<List<WeaponCartItemDTO>>(await weaponCartItemsResponse.Content.ReadAsStringAsync());
            var weaponCartItem = weaponCartItems.Where(i => i.Weapon.Id == weaponId).FirstOrDefault();

            weaponCartItem.Count.ShouldBe(1);
        }

        [Test]
        [Order(8)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task DeleteWeaponCartItem(int weaponId)
        {
            await AuthenticateUserAsync();

            var weaponCartItemsResponse = await TestClient.GetAsync("https://localhost:5001/api/WeaponCartItemsList");
            var weaponCartItems = JsonConvert.DeserializeObject<List<WeaponCartItemDTO>>(await weaponCartItemsResponse.Content.ReadAsStringAsync());
            var count = weaponCartItems.Count();

            var weaponCartItem = weaponCartItems.Where(i => i.Weapon.Id == weaponId).FirstOrDefault();

            var response = await TestClient.DeleteAsync($"https://localhost:5001/api/DeleteWeaponCartItem/{weaponCartItem.Id}");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            weaponCartItemsResponse = await TestClient.GetAsync("https://localhost:5001/api/WeaponCartItemsList");
            weaponCartItems = JsonConvert.DeserializeObject<List<WeaponCartItemDTO>>(await weaponCartItemsResponse.Content.ReadAsStringAsync());
            weaponCartItems.Count.ShouldBe(count - 1);
        }

        [Test]
        [Order(9)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task DeleteMobileSuitCartItem(int mobileSuitId)
        {
            await AuthenticateUserAsync();

            var mobileSuitCartItemsResponse = await TestClient.GetAsync("https://localhost:5001/api/MobileSuitCartItemsList");
            var mobileSuitCartItems = JsonConvert.DeserializeObject<List<MobileSuitCartItemDTO>>(await mobileSuitCartItemsResponse.Content.ReadAsStringAsync());
            var count = mobileSuitCartItems.Count();

            var mobileSuitCartItem = mobileSuitCartItems.Where(i => i.MobileSuit.Id == mobileSuitId).FirstOrDefault();

            var response = await TestClient.DeleteAsync($"https://localhost:5001/api/DeleteMobileSuitCartItem/{mobileSuitCartItem.Id}");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            mobileSuitCartItemsResponse = await TestClient.GetAsync("https://localhost:5001/api/MobileSuitCartItemsList");
            mobileSuitCartItems = JsonConvert.DeserializeObject<List<MobileSuitCartItemDTO>>(await mobileSuitCartItemsResponse.Content.ReadAsStringAsync());
            mobileSuitCartItems.Count.ShouldBe(count - 1);
        }

        [Test]
        [Order(10)]
        public async Task ClearShoppingCart()
        {
            await AuthenticateUserAsync();

            var response = await TestClient.DeleteAsync($"https://localhost:5001/api/ClearShoppingCart");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var mobileSuitCartItemsResponse = await TestClient.GetAsync("https://localhost:5001/api/MobileSuitCartItemsList");
            var mobileSuitCartItems = JsonConvert.DeserializeObject<List<MobileSuitCartItemDTO>>(await mobileSuitCartItemsResponse.Content.ReadAsStringAsync());
            var weaponCartItemsResponse = await TestClient.GetAsync("https://localhost:5001/api/WeaponCartItemsList");
            var weaponCartItems = JsonConvert.DeserializeObject<List<WeaponCartItemDTO>>(await weaponCartItemsResponse.Content.ReadAsStringAsync());

            mobileSuitCartItems.Count().ShouldBe(0);
            weaponCartItems.Count().ShouldBe(0);
        }
    }
}
