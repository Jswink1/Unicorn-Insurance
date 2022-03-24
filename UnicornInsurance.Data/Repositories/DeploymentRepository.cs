using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.Constants;
using UnicornInsurance.Application.Contracts.Data;
using UnicornInsurance.Models;

namespace UnicornInsurance.Data.Repositories
{
    public class DeploymentRepository : GenericRepository<Deployment>, IDeploymentRepository
    {
        private readonly UnicornDataDBContext _dbContext;

        public DeploymentRepository(UnicornDataDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Deployment>> GetGoodDeploymentResults()
        {
            var goodDeployments = await _dbContext.Deployments.Where(d => d.ResultType == SD.GoodDeploymentResult).ToListAsync();

            return goodDeployments;
        }

        public async Task<List<Deployment>> GetBadDeploymentResults()
        {
            var badDeployments = await _dbContext.Deployments.Where(d => d.ResultType == SD.BadDeploymentResult).ToListAsync();

            return badDeployments;
        }
    }
}
