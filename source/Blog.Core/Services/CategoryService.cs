using Blog.Core.Entities;
using Blog.Core.Interfaces;
using Blog.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IAsyncRepository<Category> _categoryRepository;

        public CategoryService(IAsyncRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async ValueTask<Category> Create(Category category)
        {
            return await _categoryRepository.AddAsync(category);
        }

        public async ValueTask DeleteById(int categoryId)
        {
            await _categoryRepository.DeleteAsync(categoryId);
        }

        public async ValueTask<IEnumerable<Category>> GetAll()
        {
            return await _categoryRepository.ListAllAsync();
        }

        public async ValueTask<Category> GetCategoryById(int categoryId)
        {
            return await _categoryRepository.GetByIdAsync(categoryId);
        }

        public async ValueTask<Category> Update(Category category)
        {
            return await _categoryRepository.UpdateAsync(category);
        }
    }
}
