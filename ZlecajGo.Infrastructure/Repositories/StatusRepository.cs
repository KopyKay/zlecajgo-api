using Microsoft.EntityFrameworkCore;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Repositories;
using ZlecajGo.Infrastructure.Persistence;

namespace ZlecajGo.Infrastructure.Repositories;

internal class StatusRepository(ZlecajGoContext dbContext) : IStatusRepository
{
    public async Task<IEnumerable<Status>> GetStatusesAsync()
    {
        var statuses = await dbContext.Statuses
            .AsNoTracking()
            .ToListAsync();

        return statuses;
    }

    public async Task<Status?> GetStatusByIdAsync(int statusId)
    {
        var status = await dbContext.Statuses
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == statusId);

        return status;
    }
}