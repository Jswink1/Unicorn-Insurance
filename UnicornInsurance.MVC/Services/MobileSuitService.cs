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
    public class MobileSuitService : BaseHttpService, IMobileSuitService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IMapper _mapper;
        private readonly IClient _httpclient;

        public MobileSuitService(IMapper mapper, IClient httpclient, ILocalStorageService localStorageService) : base(httpclient, localStorageService)
        {
            _localStorageService = localStorageService;
            _mapper = mapper;
            _httpclient = httpclient;
        }

        public async Task<MobileSuitVM> GetMobileSuitDetails(int id)
        {
            var mobileSuit = await _client.MobileSuitGETAsync(id);

            // TODO: make it so that NSwag ServiceClient does not generate FullMobileSuitDTO with "DisallowNull" Requirement on the CustomWeapon property
            return _mapper.Map<MobileSuitVM>(mobileSuit);
        }

        public async Task<List<MobileSuitVM>> GetMobileSuits()
        {
            var mobileSuits = await _client.MobileSuitAllAsync();

            return _mapper.Map<List<MobileSuitVM>>(mobileSuits);
        }

        public async Task<BaseCommandResponse> InsertMobileSuit(MobileSuitVM mobileSuitVM)
        {
            var mobileSuit = _mapper.Map<CreateFullMobileSuitDTO>(mobileSuitVM);

            return await _client.MobileSuitPOSTAsync(mobileSuit);
        }

        public async Task<BaseCommandResponse> UpdateMobileSuit(MobileSuitVM mobileSuitVM)
        {
            var mobileSuit = _mapper.Map<FullMobileSuitDTO>(mobileSuitVM);

            return await _client.MobileSuitPUTAsync(mobileSuit);
        }

        public async Task DeleteMobileSuit(int id)
        {
            await _client.MobileSuitDELETEAsync(id);
        }
    }
}
