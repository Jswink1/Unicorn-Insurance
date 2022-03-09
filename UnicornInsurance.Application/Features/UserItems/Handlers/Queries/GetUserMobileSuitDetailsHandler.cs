﻿using AutoMapper;
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
using UnicornInsurance.Application.DTOs.MobileSuit;
using UnicornInsurance.Application.DTOs.UserMobileSuit;
using UnicornInsurance.Application.DTOs.UserWeapon;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.UserItems.Requests.Queries;

namespace UnicornInsurance.Application.Features.UserItems.Handlers.Queries
{
    public class GetUserMobileSuitDetailsHandler : IRequestHandler<GetUserMobileSuitDetailsRequest, FullUserMobileSuitDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUserMobileSuitDetailsHandler(IUnitOfWork unitOfWork, 
                                               IMapper mapper,
                                               IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<FullUserMobileSuitDTO> Handle(GetUserMobileSuitDetailsRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == SD.Uid)?.Value;

            var userMobileSuit = await _unitOfWork.UserMobileSuitRepository.GetUserMobileSuit(request.Id);

            if (userMobileSuit is null)
                throw new NotFoundException(nameof(userMobileSuit), request.Id);

            if (userMobileSuit.ApplicationUserId != userId)
                throw new UnauthorizedAccessException();

            var equippedWeapons = await _unitOfWork.UserWeaponRepository.GetUserMobileSuitEquippedWeapons(userMobileSuit.Id);

            var availableWeapons = await _unitOfWork.UserWeaponRepository.GetAvailableUserWeapons(userId);

            FullUserMobileSuitDTO userMobileSuitDTO = new()
            {
                Id = userMobileSuit.Id,
                MobileSuit = _mapper.Map<MobileSuitDTO>(userMobileSuit.MobileSuit),
                EquippedWeapons = _mapper.Map<List<UserWeaponDTO>>(equippedWeapons),
                AvailableWeapons = _mapper.Map<List<UserWeaponDTO>>(availableWeapons)
            };

            return userMobileSuitDTO;
        }
    }
}