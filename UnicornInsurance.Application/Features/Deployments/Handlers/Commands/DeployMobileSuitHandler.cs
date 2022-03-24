using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnicornInsurance.Application.Constants;
using UnicornInsurance.Application.Contracts.Data;
using UnicornInsurance.Application.DTOs.Deployment;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.Deployments.Requests.Commands;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Application.Features.Deployments.Handlers.Commands
{
    public class DeployMobileSuitHandler : IRequestHandler<DeployMobileSuitCommand, DeploymentDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeployMobileSuitHandler(IUnitOfWork unitOfWork,
                                       IMapper mapper,
                                       IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<DeploymentDTO> Handle(DeployMobileSuitCommand request, CancellationToken cancellationToken)
        {
            var deployment = new DeploymentDTO();

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == SD.Uid)?.Value;

            var userMobileSuit = await _unitOfWork.UserMobileSuitRepository.GetUserMobileSuit(request.UserMobileSuitId);

            if (userMobileSuit is null)
                throw new NotFoundException(nameof(userMobileSuit), request.UserMobileSuitId);
            if (userMobileSuit.ApplicationUserId != userId)
                throw new UnauthorizedAccessException();

            if (userMobileSuit.IsDamaged)
                throw new MobileSuitDamagedException();

            var userMobileSuitEquippedWeapon = await _unitOfWork.UserWeaponRepository.GetUserMobileSuitEquippedWeapon(request.UserMobileSuitId);
            var userMobileSuitCustomWeapon = await _unitOfWork.UserWeaponRepository.GetUserMobileSuitCustomWeapon(request.UserMobileSuitId);

            Random rnd = new();
            int badResultProbability = 100;

            // If the UserMobileSuit has an equipped weapon or custom weapon,
            // reduce the probability of a bad deployment result by 5%
            if (userMobileSuitEquippedWeapon is not null)
            {
                badResultProbability -= 5;
            }
            if (userMobileSuitCustomWeapon is not null)
            {
                badResultProbability -= 5;
            }
             
            int probabilityResult = rnd.Next(0, badResultProbability);

            // Deployment Result is Good
            if (probabilityResult < 50)
            {
                // Get the list of good deployment results
                var goodDeploymentResults = await _unitOfWork.DeploymentRepository.GetGoodDeploymentResults();

                // Select a random result
                int randomIndex = rnd.Next(0, goodDeploymentResults.Count);
                var randomResult = goodDeploymentResults[randomIndex];

                deployment = _mapper.Map<DeploymentDTO>(randomResult);
            }
            // Deployment Result is Bad
            else if (probabilityResult >= 50)
            {
                // Get the list of bad deployment results
                var badDeploymentResults = await _unitOfWork.DeploymentRepository.GetBadDeploymentResults();

                // Select a random result
                int randomIndex = rnd.Next(0, badDeploymentResults.Count);
                var randomResult = badDeploymentResults[randomIndex];

                deployment = _mapper.Map<DeploymentDTO>(randomResult);

                // If user has no insurance
                if (userMobileSuit.EndOfCoverage < DateTime.Now)
                {
                    // Set the mobile suit damage status
                    userMobileSuit.IsDamaged = true;

                    // Delete the equipped weapons
                    if (userMobileSuitEquippedWeapon is not null)
                        await _unitOfWork.UserWeaponRepository.Delete(userMobileSuitEquippedWeapon);
                    if (userMobileSuitCustomWeapon is not null)
                        await _unitOfWork.UserWeaponRepository.Delete(userMobileSuitCustomWeapon);
                }
                // If user has insurance
                else
                {
                    // If user has standard insurance plan
                    if (userMobileSuit.InsurancePlan == SD.StandardInsurancePlan)
                    {
                        // Delete the equipped weapons
                        if (userMobileSuitEquippedWeapon is not null)
                            await _unitOfWork.UserWeaponRepository.Delete(userMobileSuitEquippedWeapon);
                        if (userMobileSuitCustomWeapon is not null)
                            await _unitOfWork.UserWeaponRepository.Delete(userMobileSuitCustomWeapon);
                    }
                    // If the user has super insurance plan
                    else if (userMobileSuit.InsurancePlan == SD.SuperInsurancePlan)
                    {
                        // Delete only the equipped custom weapon
                        if (userMobileSuitCustomWeapon is not null)
                            await _unitOfWork.UserWeaponRepository.Delete(userMobileSuitCustomWeapon);
                    }
                }
            }

            await _unitOfWork.Save();
            return deployment;
        }
    }
}
