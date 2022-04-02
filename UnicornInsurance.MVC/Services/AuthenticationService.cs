using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Constants;
using UnicornInsurance.MVC.Contracts;
using UnicornInsurance.MVC.Models;
using UnicornInsurance.MVC.Services.Base;

namespace UnicornInsurance.MVC.Services
{
    public class AuthenticationService : BaseHttpService, Contracts.IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IOptions<ClientVerifyEmail> _clientVerifyEmail;
        private JwtSecurityTokenHandler _tokenHandler;

        public AuthenticationService(IClient client, 
                                     ILocalStorageService localStorage, 
                                     IHttpContextAccessor httpContextAccessor,
                                     IMapper mapper,
                                     IOptions<ClientVerifyEmail> clientVerifyEmail)
                                     : base(client, localStorage)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _clientVerifyEmail = clientVerifyEmail;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public async Task<bool> Authenticate(LoginVM login)
        {
            try
            {
                AuthRequest authenticationRequest = new() { Email = login.Email, Password = login.Password };
                var authenticationResponse = await _client.LoginAsync(authenticationRequest);

                if (authenticationResponse.Token != string.Empty)
                {
                    //Get Claims from token and Build auth user object
                    var tokenContent = _tokenHandler.ReadJwtToken(authenticationResponse.Token);
                    var claims = ParseClaims(tokenContent);
                    var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                    var loginResult = _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
                    _localStorage.SetStorageValue(SD.Token, authenticationResponse.Token);

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> Register(RegisterVM registration)
        {
            RegistrationRequest registrationRequest = _mapper.Map<RegistrationRequest>(registration);
            registrationRequest.ClientVerifyEmailURL = _clientVerifyEmail.Value.URL;

            var response = await _client.RegisterAsync(registrationRequest);

            if (!string.IsNullOrEmpty(response.UserId))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> VerifyEmail(string token)
        {
            var response = await _client.VerifyEmailAsync(token);

            return response.Success;
        }

        public async Task Logout()
        {
            _localStorage.ClearStorage(new List<string> { SD.Token });
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
        {
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            return claims;
        }
    }
}
