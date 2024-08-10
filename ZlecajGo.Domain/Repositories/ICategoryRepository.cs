using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Domain.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
}