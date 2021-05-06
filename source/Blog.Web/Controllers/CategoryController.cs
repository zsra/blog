using Blog.Core.Entities;
using Blog.Core.Interfaces.Services;
using Blog.Web.Converters;
using Blog.Web.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{
    [ApiController]
    [Route("api/v1/public/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("all")]
        public async ValueTask<IEnumerable<CategoryDTO>> GetAll()
        {
            IEnumerable<Category> categories = await _categoryService.GetAll();
            return categories.Select(category => category.EntityToDto());
        }

        [HttpGet("{categoryId}")]
        public async ValueTask<CategoryDTO> Get(int categoryId)
        {
            Category category = await _categoryService.GetCategoryById(categoryId);
            return category.EntityToDto();
        }

        [HttpPost("create")]
        public async ValueTask<CategoryDTO> Create([FromBody]CategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                Category result = await _categoryService.Create(category.DtoToEntity());
                return result.EntityToDto();
            }

            return category;
        }

        [HttpPut("update")]
        public async ValueTask<CategoryDTO> Update([FromBody] CategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                Category result = await _categoryService.Update(category.DtoToEntity());
                return result.EntityToDto();
            }

            return category;
        }

        [HttpDelete("{categoryId}")]
        public async ValueTask<string> Delete(int categoryId)
        {
            await _categoryService.DeleteById(categoryId);
            return categoryId.ToString();
        }
    }
}
