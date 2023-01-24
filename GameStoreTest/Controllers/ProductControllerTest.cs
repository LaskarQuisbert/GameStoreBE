using AutoMapper;
using GameStore.Controllers;
using GameStore.Models;
using GameStore.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GameStoreTest.Controllers
{
    public class ProductControllerTest
    {
        private readonly IEnumerable<Product> _mockProducts;

        private readonly ProductController _controller;
        private readonly Mock<IProductService> _mockProductService = new Mock<IProductService>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

        public ProductControllerTest() 
        {
            _controller = new ProductController(_mockProductService.Object, _mockMapper.Object);

            _mockProducts= new List<Product> 
            {
                new Product 
                {
                    Id = 1,
                    Price= 1,
                    AgeRestriction= 1,
                    Company="",
                    Description="",
                    Name="Test Product 1"
                },
                new Product
                {
                    Id = 2,
                    Price= 2,
                    AgeRestriction= 2,
                    Company="",
                    Description="",
                    Name="Test Product 2"
                }
            };
        }

        [Fact]
        public async Task Get_ReturnsAProductsList() 
        {
            _mockProductService.Setup(x => x.GetAll()).Returns(Task.FromResult(_mockProducts));

            var result = await _controller.GetAll();
            //var a = result.Result;

            Assert.NotNull(result);
            //result.Result.StatusCode.Value.;
        }
    }
}
