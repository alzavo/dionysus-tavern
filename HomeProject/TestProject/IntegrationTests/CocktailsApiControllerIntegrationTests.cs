using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using PublicApi.DTO.v1;
using TestProject.Helpers;
using WebApp;
using Xunit;
using Xunit.Abstractions;

namespace TestProject.IntegrationTests
{
    public class CocktailsApiControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly HttpClient _client;

        public CocktailsApiControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
        {
            _factory = factory;
            _testOutputHelper = testOutputHelper;
            _client = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.UseSetting("test_database_name", Guid.NewGuid().ToString());
                })
                .CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });
        }

        [Fact]
        public async Task Get_All_Cocktails()
        {
            // ARRANGE
            const string uriAllCocktails = "/api/v1/Cocktails";
            const string uriRegister = "/api/v1/Account/Register";
            var registerDto = new Register
            {
                Username = "test",
                Email = "test@gmail.com",
                Password = "Qwe!23"
            };
            StringContent httpContent = 
                new(JsonConvert.SerializeObject(registerDto), System.Text.Encoding.UTF8, "application/json");
            
            
            // ACT 1
            var getRegisterResponse = await _client.PostAsync(uriRegister, httpContent);
            var content = getRegisterResponse.Content.ReadAsStringAsync().Result;
            // _testOutputHelper.WriteLine(content);
            
            var jwtResponse = JsonHelper.DeserializeWithWebDefaults<JwtResponse>(content);

            // ASSERT 1
            Assert.NotNull(jwtResponse);
            Assert.NotNull(jwtResponse!.Token);
            Assert.Contains("test", jwtResponse!.Username);
            
            
            // ACT 1 -> 2
            _client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", jwtResponse.Token);
            var getAllCocktailsResponse = await _client.GetAsync(uriAllCocktails);
            content = getAllCocktailsResponse.Content.ReadAsStringAsync().Result;
            // _testOutputHelper.WriteLine(content);
            
            var cocktailsList = JsonHelper.DeserializeWithWebDefaults<List<DAL.App.DTO.Cocktail>>(content);
            
            // ASSERT 1 -> 2
            Assert.NotNull(cocktailsList);
            Assert.Equal(3, cocktailsList!.Count);
            Assert.Contains("Long Drink", cocktailsList[0].Name);
            Assert.True(cocktailsList[0].Alcoholic);
            Assert.Contains("Margarita", cocktailsList[1].Name);
            Assert.Contains("Black Russian", cocktailsList[2].Name);
        }

    }
}
