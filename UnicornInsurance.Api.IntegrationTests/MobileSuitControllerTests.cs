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
using UnicornInsurance.Application.DTOs.MobileSuit;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Api.IntegrationTests
{
    public class MobileSuitControllerTests : IntegrationTest
    {
        [Test]
        [Order(1)]
        public async Task GetMobileSuitsList()
        {
            await AuthenticateUserAsync();

            var response = await TestClient.GetAsync("https://localhost:5001/api/mobilesuit");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            var mobileSuits = JsonConvert.DeserializeObject<List<MobileSuitDTO>>(jsonString);

            mobileSuits.Count.ShouldBe(8);
        }

        [Test]
        [Order(2)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task GetMobileSuit(int mobileSuitId)
        {
            await AuthenticateUserAsync();

            var response = await TestClient.GetAsync($"https://localhost:5001/api/weapon/{mobileSuitId}");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            var mobileSuit = JsonConvert.DeserializeObject<MobileSuitDTO>(jsonString);

            mobileSuit.ShouldNotBeNull();
        }

        [Test]
        [Order(3)]
        public async Task CreateMobileSuit()
        {
            await AuthenticateAdminAsync();

            CreateMobileSuitDTO mobileSuitDTO = new()
            {
                Name = "new mobile suit",
                Description = "description",
                Price = 1000                
            };

            var response = await TestClient.PostAsJsonAsync($"https://localhost:5001/api/mobilesuit", mobileSuitDTO);

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
        public async Task UpdateWeapon(int mobileSuitId)
        {
            await AuthenticateAdminAsync();

            MobileSuitDTO mobileSuitDTO = new()
            {
                Id = mobileSuitId,
                Name = "updated mobile suit",
                Description = "updated description",
                Price = 2000
            };

            var response = await TestClient.PutAsJsonAsync($"https://localhost:5001/api/mobilesuit", mobileSuitDTO);

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
        public async Task DeleteMobileSuit(int mobileSuitId)
        {
            await AuthenticateAdminAsync();

            var mobileSuitListResponse = await TestClient.GetAsync("https://localhost:5001/api/mobilesuit");
            var mobileSuits = JsonConvert.DeserializeObject<List<MobileSuitDTO>>(await mobileSuitListResponse.Content.ReadAsStringAsync());
            var count = mobileSuits.Count;

            var response = await TestClient.DeleteAsync($"https://localhost:5001/api/mobilesuit/{mobileSuitId}");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            mobileSuitListResponse = await TestClient.GetAsync("https://localhost:5001/api/mobilesuit");
            mobileSuits = JsonConvert.DeserializeObject<List<MobileSuitDTO>>(await mobileSuitListResponse.Content.ReadAsStringAsync());

            mobileSuits.Count.ShouldBe(count - 1);
        }
    }
}
