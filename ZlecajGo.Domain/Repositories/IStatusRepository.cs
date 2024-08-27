using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Domain.Repositories;

public interface IStatusRepository
{
    Task<IEnumerable<Status>> GetStatusesAsync();
    Task<Status?> GetStatusByIdAsync(int statusId);
}