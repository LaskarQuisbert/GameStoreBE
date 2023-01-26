using GameStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Net;
using System.Net.Cache;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GameStore.IntegrationTest
{
    [TestClass]
    public class ProductControllerTest
    {
        private HttpClient _httpClient;
        private Product _newProduct;

        public ProductControllerTest() 
        {
            var appfactory = new WebApplicationFactory<Program>();
            _httpClient = appfactory.CreateClient();

            _newProduct= new Product 
            {
                Id= 0,
                Name="Test New Name",
                Price=20,
                Company="Test New Company",
                AgeRestriction=18,
                Description="Test new description"
            };
        }

        [TestMethod]
        public async Task Post_CreateNew_Success()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/v1/product");
            request.Content = new StringContent(JsonSerializer.Serialize(_newProduct), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);

            var stringResult = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Product>(stringResult, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            Assert.IsNotNull(stringResult);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsInstanceOfType(result, typeof(Product));
            Assert.AreEqual("http://localhost/api/v1/Product/" + result.Id, response.Headers?.Location?.AbsoluteUri);
        }

        [TestMethod]
        public async Task Get_All_Success()
        {
            var response = await _httpClient.GetAsync("/api/v1/product");
            response.EnsureSuccessStatusCode();
            
            var stringResponse = await response.Content.ReadAsStringAsync();
            var products = JsonSerializer.Deserialize<List<Product>>(stringResponse, new JsonSerializerOptions 
                    { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            Assert.IsNotNull(response.Content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsInstanceOfType(products, typeof(List<Product>));
        }
    }
}