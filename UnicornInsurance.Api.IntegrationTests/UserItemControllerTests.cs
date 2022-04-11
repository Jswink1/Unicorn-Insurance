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
using UnicornInsurance.Application.Constants;
using UnicornInsurance.Application.DTOs.UserMobileSuit;
using UnicornInsurance.Application.DTOs.UserWeapon;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Api.IntegrationTests
{
    public class UserItemControllerTests : IntegrationTest
    {
        [Test]
        [Order(1)]
        public async Task GetUserMobileSuitsList()
        {
            await AuthenticateUserAsync();

            var response = await TestClient.GetAsync("https://localhost:5001/api/UserMobileSuit");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            var userMobileSuits = JsonConvert.DeserializeObject<List<UserMobileSuitDTO>>(jsonString);

            userMobileSuits.Count.ShouldBe(1);
        }

        [Test]
        [Order(2)]
        public async Task GetUserMobileSuit()
        {
            await AuthenticateUserAsync();

            var response = await TestClient.GetAsync("https://localhost:5001/api/UserMobileSuit/1");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            var userMobileSuit = JsonConvert.DeserializeObject<UserMobileSuitDTO>(jsonString);

            userMobileSuit.ShouldNotBeNull();
        }

        [Test]
        [Order(3)]
        public async Task EquipWeapon()
        {
            await AuthenticateUserAsync();

            EquipWeaponDTO equipWeaponDTO = new()
            {
                SelectedWeaponId = 1,
                UserMobileSuitId = 1
            };

            var response = await TestClient.PutAsJsonAsync("https://localhost:5001/api/EquipWeapon", equipWeaponDTO);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            var baseCommandResponse = JsonConvert.DeserializeObject<BaseCommandResponse>(jsonString);

            baseCommandResponse.Success.ShouldBeTrue();
        }

        [Test]
        [Order(4)]
        public async Task UnequipWeapon()
        {
            await AuthenticateUserAsync();

            var response = await TestClient.PutAsJsonAsync("https://localhost:5001/api/UnequipWeapon", 1);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Test]
        [Order(5)]
        public async Task UserPurchaseInsurance()
        {
            await AuthenticateUserAsync();

            UserInsurancePlanDTO userInsurancePlanDTO = new()
            {
                InsurancePlan = SD.UltraInsurancePlan,
                UserMobileSuitId = 1
            };

            var response = await TestClient.PutAsJsonAsync("https://localhost:5001/api/UserInsurancePlan", userInsurancePlanDTO);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            var baseCommandResponse = JsonConvert.DeserializeObject<BaseCommandResponse>(jsonString);

            baseCommandResponse.Success.ShouldBeTrue();
        }

        [Test]
        [Order(6)]
        public async Task DeleteUserMobileSuit()
        {
            await AuthenticateUserAsync();

            var response = await TestClient.DeleteAsync("https://localhost:5001/api/UserMobileSuit/1");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}
