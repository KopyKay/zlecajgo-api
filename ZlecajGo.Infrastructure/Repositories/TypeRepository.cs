using Microsoft.EntityFrameworkCore;
using ZlecajGo.Domain.Repositories;
using ZlecajGo.Infrastructure.Persistence;
using Type = ZlecajGo.Domain.Entities.Type;

namespace ZlecajGo.Infrastructure.Repositories;

internal class TypeRepository(ZlecajGoContext dbContext) : ITypeRepository
{
    public async Task<IEnumerable<Type>> GetTypesAsync()
    {
        var types = await dbContext.Types
            .AsNoTracking()
            .ToListAsync();

        return types;
    }

    public async Task<Type?> GetTypeByIdAsync(int typeId)
    {
        var type = await dbContext.Types
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == typeId);

        return type;
    }
}