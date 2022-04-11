using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using UnicornInsurance.Data;
using UnicornInsurance.Identity;
using UnicornInsurance.Application.Models.Identity;
using System.Net.Http.Json;

namespace UnicornInsurance.Api.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        private readonly string dataDbName;
        private readonly string identityDbName;

        protected IntegrationTest()
        {
            dataDbName = Guid.NewGuid().ToString();
            identityDbName = Guid.NewGuid().ToString();

            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder => 
                { 
                    builder.ConfigureServices(services => 
                    { 
                        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<UnicornDataDBContext>));                         
                        if (descriptor != null) 
                        {
                            services.RemoveAll(typeof(DbContextOptions<UnicornDataDBContext>));
                        }

                        descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<UnicornIdentityDBContext>));
                        if (descriptor != null)
                        {
                            services.RemoveAll(typeof(DbContextOptions<UnicornIdentityDBContext>));
                        }

                        services.AddDbContext<UnicornDataDBContext>(options => { options.UseInMemoryDatabase(dataDbName); });
                        services.AddDbContext<UnicornIdentityDBContext>(options => { options.UseInMemoryDatabase(identityDbName); });
                    }); 
                }); 
            
            TestClient = appFactory.CreateClient();

            //var appFactory = new WebApplicationFactory<Startup>()
            //    .WithWebHostBuilder(builder =>
            //    {
            //        builder.ConfigureServices(services =>
            //        {
            //            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DataContext>));

            //            services.RemoveAll(typeof(DbContextOptions<UnicornDataDBContext>));
            //            services.RemoveAll(typeof(DbContextOptions<UnicornIdentityDBContext>));
            //            services.AddDbContext<UnicornDataDBContext>(options => { options.UseInMemoryDatabase("TestDataDb"); });
            //            services.AddDbContext<UnicornIdentityDBContext>(options => { options.UseInMemoryDatabase("TestIdentityDb"); });
            //        });
            //    });

            //TestClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateUserAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetUserJwtAsync());
        }

        protected async Task AuthenticateAdminAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetAdminJwtAsync());
        }

        private async Task<string> GetUserJwtAsync()
        {
            SeedDataBase();

            var response = await TestClient.PostAsJsonAsync("https://localhost:5001/api/account/login", new AuthRequest
            {
                Email = "user@user.com",
                Password = "password"
            });

            var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();

            return authResponse.Token;
        }

        private async Task<string> GetAdminJwtAsync()
        {
            SeedDataBase();

            var response = await TestClient.PostAsJsonAsync("https://localhost:5001/api/account/login", new AuthRequest
            {
                Email = "admin@user.com",
                Password = "password"
            });

            var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();

            return authResponse.Token;
        }

        private void SeedDataBase()
        {
            var identityDbOptions = new DbContextOptionsBuilder<UnicornIdentityDBContext>()
                            .UseInMemoryDatabase(databaseName: identityDbName)
                            .Options;

            using (var context = new UnicornIdentityDBContext(identityDbOptions))
            {
                context.Database.EnsureCreated();
            }

            var dataDbOptions = new DbContextOptionsBuilder<UnicornDataDBContext>()
                            .UseInMemoryDatabase(databaseName: dataDbName)
                            .Options;

            using (var context = new UnicornDataDBContext(dataDbOptions))
            {
                context.Database.EnsureCreated();
            }
        }
    }
}
