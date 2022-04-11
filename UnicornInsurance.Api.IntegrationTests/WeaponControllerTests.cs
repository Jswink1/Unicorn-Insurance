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
using UnicornInsurance.Application.DTOs.Weapon;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Api.IntegrationTests
{
    public class WeaponControllerTests : IntegrationTest
    {
        [Test]
        [Order(1)]
        public async Task GetWeaponsList()
        {
            await AuthenticateUserAsync();

            var response = await TestClient.GetAsync("https://localhost:5001/api/weapon");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            var weapons = JsonConvert.DeserializeObject<List<WeaponDTO>>(jsonString);

            weapons.Count.ShouldBe(5);
        }

        [Test]
        [Order(2)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task GetWeapon(int weaponId)
        {
            await AuthenticateUserAsync();

            var response = await TestClient.GetAsync($"https://localhost:5001/api/weapon/{weaponId}");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            var weapon = JsonConvert.DeserializeObject<WeaponDTO>(jsonString);

            weapon.ShouldNotBeNull();
        }

        [Test]
        [Order(3)]
        public async Task CreateWeapon()
        {
            await AuthenticateAdminAsync();

            CreateWeaponDTO weaponDTO = new()
            {
                Name = "new weapon",
                Description = "description",
                Price = 1000
            };

            var response = await TestClient.PostAsJsonAsync($"https://localhost:5001/api/weapon", weaponDTO);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            var baseCommandResponse = JsonConvert.DeserializeObject<BaseCommandResponse>(jsonString);

            baseCommandResponse.Success.ShouldBeTrue();
        }

        [Test]
        [Order(4)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task UpdateWeapon(int weaponId)
        {
            await AuthenticateAdminAsync();

            WeaponDTO weaponDTO = new()
            {
                Id = weaponId,
                Name = "updated weapon",
                Description = "updated description",
                Price = 2000
            };

            var response = await TestClient.PutAsJsonAsync($"https://localhost:5001/api/weapon", weaponDTO);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            var baseCommandResponse = JsonConvert.DeserializeObject<BaseCommandResponse>(jsonString);

            baseCommandResponse.Success.ShouldBeTrue();
        }

        [Test]
        [Order(5)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task DeleteWeapon(int weaponId)
        {
            await AuthenticateAdminAsync();

            var weaponsListResponse = await TestClient.GetAsync("https://localhost:5001/api/weapon");
            var weapons = JsonConvert.DeserializeObject<List<WeaponDTO>>(await weaponsListResponse.Content.ReadAsStringAsync());
            var count = weapons.Count;

            var response = await TestClient.DeleteAsync($"https://localhost:5001/api/weapon/{weaponId}");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            weaponsListResponse = await TestClient.GetAsync("https://localhost:5001/api/weapon");
            weapons = JsonConvert.DeserializeObject<List<WeaponDTO>>(await weaponsListResponse.Content.ReadAsStringAsync());

            weapons.Count.ShouldBe(count - 1);
        }
    }
}
