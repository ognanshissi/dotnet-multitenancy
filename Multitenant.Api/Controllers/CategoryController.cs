using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Multitenant.Api.Models;

namespace Multitenant.Api.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController: ControllerBase
    {

        private readonly ICategoryService _categoryService;
        
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        
        [HttpGet]
        public async Task<IActionResult> All()
        {
            return Ok(await _categoryService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _categoryService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequest request)
        {
            return Ok(await _categoryService.CreateAsync(request.Name));
        }
    }
}