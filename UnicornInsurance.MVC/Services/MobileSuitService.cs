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

        public async Task<MobileSuit> GetMobileSuitDetails(int id)
        {
            AddBearerToken();
            var mobileSuit = await _client.MobileSuitGETAsync(id);

            return _mapper.Map<MobileSuit>(mobileSuit);
        }

        public async Task<List<MobileSuit>> GetMobileSuits()
        {
            AddBearerToken();
            var mobileSuits = await _client.MobileSuitAllAsync();

            return _mapper.Map<List<MobileSuit>>(mobileSuits);
        }

        public async Task<BaseCommandResponse> InsertMobileSuit(MobileSuit mobileSuitVM)
        {
            AddBearerToken();
            var mobileSuit = _mapper.Map<CreateFullMobileSuitDTO>(mobileSuitVM);

            return await _client.MobileSuitPOSTAsync(mobileSuit);
        }

        public async Task<BaseCommandResponse> UpdateMobileSuit(MobileSuit mobileSuitVM)
        {
            AddBearerToken();
            var mobileSuit = _mapper.Map<FullMobileSuitDTO>(mobileSuitVM);

            return await _client.MobileSuitPUTAsync(mobileSuit);
        }

        public async Task DeleteMobileSuit(int id)
        {
            AddBearerToken();
            await _client.MobileSuitDELETEAsync(id);
        }
    }
}
