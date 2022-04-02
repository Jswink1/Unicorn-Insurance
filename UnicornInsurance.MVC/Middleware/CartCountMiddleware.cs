using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Constants;
using UnicornInsurance.MVC.Contracts;

namespace UnicornInsurance.MVC.Middleware
{
    public class CartCountMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILocalStorageService _localStorageService;

        public CartCountMiddleware(RequestDelegate next, 
                                   ILocalStorageService localStorageService)
        {
            _next = next;
            _localStorageService = localStorageService;
        }

        public async Task InvokeAsync(HttpContext httpContext,
                                      IShoppingCartService shoppingCartService)
        {
            try
            {
                // Check if the user has a token in local storage
                var tokenExists = _localStorageService.Exists(SD.Token);

                // If the user does have a token
                var tokenIsValid = true;
                if (tokenExists)
                {
                    var token = _localStorageService.GetStorageValue<string>(SD.Token);
                    JwtSecurityTokenHandler tokenHandler = new();
                    var tokenContent = tokenHandler.ReadJwtToken(token);
                    var expiry = tokenContent.ValidTo;

                    // Check if the token is expired
                    if (expiry < DateTime.Now)
                    {
                        tokenIsValid = false;
                    }
                }

                // If the user has no token or the token is expired
                if (tokenIsValid == false || tokenExists == false)
                {
                    //await SignOutAndRedirect(httpContext);
                    httpContext.Session.SetInt32(SD.CartSesh, 0);
                    await _next(httpContext);
                    return;
                }
                else
                {
                    var count = shoppingCartService.GetShoppingCartItemCount().Result.ShoppingCartItemCount;

                    httpContext.Session.SetInt32(SD.CartSesh, count);
                }

                // Move on to the next request
                await _next(httpContext);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private static async Task SignOutAndRedirect(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var path = $"/users/login";
            httpContext.Response.Redirect(path);
        }
    }
}
