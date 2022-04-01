using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Multitenant.Api.Models;

namespace Multitenant.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return Ok(await _productService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _productService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            return Ok(await _productService.CreateAsync(request.Name, request.Description, request.Rate));
        }
    }
}