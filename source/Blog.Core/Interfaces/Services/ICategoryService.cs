using Blog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Interfaces.Services
{
    public interface ICategoryService
    {
        ValueTask<IEnumerable<Category>> GetAll();
        ValueTask<Category> GetCategoryById(int categoryId);
        ValueTask<Category> Create(Category category);
        ValueTask<Category> Update(Category category);
        ValueTask DeleteById(int categoryId);
    }
}
