using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnicornInsurance.Application.Contracts.Data;
using UnicornInsurance.Application.DTOs.MobileSuit.Validators;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.MobileSuits.Requests.Commands;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Application.Features.MobileSuits.Handlers.Commands
{
    public class UpdateMobileSuitHandler : IRequestHandler<UpdateMobileSuitCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateMobileSuitHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateMobileSuitCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateMobileSuitDTOValidator();
            var validationResult = await validator.ValidateAsync(request.MobileSuitDTO);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Mobile Suit Update Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var mobileSuit = await _unitOfWork.MobileSuitRepository.GetFullMobileSuitDetails(request.MobileSuitDTO.Id);

                if (mobileSuit is null)
                    throw new NotFoundException(nameof(mobileSuit), request.MobileSuitDTO.Id);

                _mapper.Map(request.MobileSuitDTO, mobileSuit);

                if (request.MobileSuitDTO.CustomWeapon is not null)
                    mobileSuit.CustomWeapon.IsCustomWeapon = true;

                await _unitOfWork.MobileSuitRepository.Update(mobileSuit);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Mobile Suit Update Successful";
                response.Id = mobileSuit.Id;
            }

            return response;
        }
    }
}
