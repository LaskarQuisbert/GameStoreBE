using AutoMapper;
using GameStore.Models;
using GameStore.Models.Dtos;
using GameStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace GameStore.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ODataController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper) 
        {
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var result = await _productService.GetById(id);
            return Ok(_mapper.Map<ProductDto>(result));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDto value)
        {
            if (value is null)
                return BadRequest(new ArgumentNullException());

            var product = await _productService.GetById(value.Id);
            var result = await _productService.SaveUpdate(_mapper.Map<Product>(value));
            if (product == null)
                return CreatedAtAction(nameof(Get), new {id = result.Id}, result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try 
            {
                await _productService.Delete(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
