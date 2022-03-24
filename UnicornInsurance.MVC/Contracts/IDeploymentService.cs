using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Models;
using UnicornInsurance.MVC.Services.Base;

namespace UnicornInsurance.MVC.Contracts
{
    public interface IDeploymentService
    {
        Task<BaseCommandResponse> InsertDeployment(Deployment deployment);
        Task<BaseCommandResponse> UpdateDeployment(Deployment deployment);
        Task<Deployment> GetDeploymentDetails(int id);
        Task<List<Deployment>> GetDeployments();
        Task DeleteDeployment(int id);
        Task<Deployment> DeployMobileSuit(int id);
    }
}
