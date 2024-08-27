using Type = ZlecajGo.Domain.Entities.Type;

namespace ZlecajGo.Domain.Repositories;

public interface ITypeRepository
{
    Task<IEnumerable<Type>> GetTypesAsync();
    Task<Type?> GetTypeByIdAsync(int typeId);
}