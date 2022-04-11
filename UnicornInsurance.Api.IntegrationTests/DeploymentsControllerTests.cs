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
using UnicornInsurance.Application.DTOs.Deployment;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Api.IntegrationTests
{
    public class DeploymentsControllerTests : IntegrationTest
    {
        [Test]
        [Order(1)]
        public async Task GetDeploymentsList()
        {
            await AuthenticateAdminAsync();

            var response = await TestClient.GetAsync("https://localhost:5001/api/deployment");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            var deployments = JsonConvert.DeserializeObject<List<DeploymentDTO>>(jsonString);

            deployments.Count.ShouldBe(30);
        }

        [Test]
        [Order(2)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task GetDeployment(int deploymentId)
        {
            await AuthenticateAdminAsync();

            var response = await TestClient.GetAsync($"https://localhost:5001/api/deployment/{deploymentId}");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            var deployment = JsonConvert.DeserializeObject<DeploymentDTO>(jsonString);

            deployment.ShouldNotBeNull();
        }

        [Test]
        [Order(3)]
        public async Task CreateDeployment()
        {
            await AuthenticateAdminAsync();

            CreateDeploymentDTO createDeploymentDTO = new()
            {
                ResultType = SD.BadDeploymentResult,
                Description = "new deployment"
            };

            var response = await TestClient.PostAsJsonAsync($"https://localhost:5001/api/deployment", createDeploymentDTO);

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
        public async Task UpdateDeployment(int deploymentId)
        {
            await AuthenticateAdminAsync();

            DeploymentDTO deploymentDTO = new()
            {
                Id = deploymentId,
                ResultType = SD.GoodDeploymentResult,
                Description = "updated deployment"
            };

            var response = await TestClient.PutAsJsonAsync($"https://localhost:5001/api/deployment", deploymentDTO);

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
        public async Task DeleteDeployment(int deploymentId)
        {
            await AuthenticateAdminAsync();

            var deploymentListResponse = await TestClient.GetAsync("https://localhost:5001/api/deployment");
            var deployments = JsonConvert.DeserializeObject<List<DeploymentDTO>>(await deploymentListResponse.Content.ReadAsStringAsync());
            var count = deployments.Count;

            var response = await TestClient.DeleteAsync($"https://localhost:5001/api/deployment/{deploymentId}");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            deploymentListResponse = await TestClient.GetAsync("https://localhost:5001/api/deployment");
            deployments = JsonConvert.DeserializeObject<List<DeploymentDTO>>(await deploymentListResponse.Content.ReadAsStringAsync());

            deployments.Count.ShouldBe(count - 1);
        }

        [Test]
        [Order(6)]
        [TestCase(1)]
        public async Task DeployMobileSuit(int userMobileSuitId)
        {
            await AuthenticateUserAsync();

            var response = await TestClient.PostAsJsonAsync($"https://localhost:5001/api/deployment/DeployMobileSuit", userMobileSuitId);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            var deployment = JsonConvert.DeserializeObject<DeploymentDTO>(jsonString);

            deployment.ShouldNotBeNull();
        }
    }
}
