using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlinePasswordManager.Server.Services.CategoryService;
using OnlinePasswordManager.Shared.Models.DTO;

namespace OnlinePasswordManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetAll()
        {
            var categories = await _categoryService.GetAll();
        
            return Ok(categories);
        }

        [HttpPut]
        public async Task<ActionResult> Create([FromBody] CategoryCreateDTO dto)
        {
            await _categoryService.Create(dto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute]int id)
        {
            await _categoryService.Delete(id);
            
            return Ok();
        }

    }
}
