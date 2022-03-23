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
    public class DeploymentService : BaseHttpService, IDeploymentService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IMapper _mapper;
        private readonly IClient _httpclient;

        public DeploymentService(IMapper mapper, IClient httpclient, ILocalStorageService localStorageService) : base(httpclient, localStorageService)
        {
            _localStorageService = localStorageService;
            _mapper = mapper;
            _httpclient = httpclient;
        }

        public async Task<List<Deployment>> GetDeployments()
        {
            var deploymentDTOs = await _client.DeploymentAllAsync();

            return _mapper.Map<List<Deployment>>(deploymentDTOs);
        }

        public async Task<Deployment> GetDeploymentDetails(int id)
        {
            var deploymentDTO = await _client.DeploymentGETAsync(id);

            return _mapper.Map<Deployment>(deploymentDTO);
        }

        public async Task<BaseCommandResponse> InsertDeployment(Deployment deployment)
        {
            var deploymentDTO = _mapper.Map<CreateDeploymentDTO>(deployment);

            return await _client.DeploymentPOSTAsync(deploymentDTO);
        }

        public async Task<BaseCommandResponse> UpdateDeployment(Deployment deployment)
        {
            var deploymentDTO = _mapper.Map<DeploymentDTO>(deployment);

            return await _client.DeploymentPUTAsync(deploymentDTO);
        }

        public async Task DeleteDeployment(int id)
        {
            await _client.DeploymentDELETEAsync(id);
        }
    }
}
