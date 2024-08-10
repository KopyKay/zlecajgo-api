using Microsoft.EntityFrameworkCore;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Repositories;
using ZlecajGo.Infrastructure.Persistence;

namespace ZlecajGo.Infrastructure.Repositories;

internal class CategoryRepository(ZlecajGoContext dbContext) : ICategoryRepository
{
    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        var categories = await dbContext.Categories.ToListAsync();

        return categories;
    }
}