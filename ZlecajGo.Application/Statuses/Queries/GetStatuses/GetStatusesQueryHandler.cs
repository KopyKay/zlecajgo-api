using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Statuses.Dtos;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Statuses.Queries.GetStatuses;

public class GetStatusesQueryHandler
(
    ILogger<GetStatusesQueryHandler> logger,
    IStatusRepository statusRepository,
    IMapper mapper
)
: IRequestHandler<GetStatusesQuery, IEnumerable<StatusDto>>
{
    public async Task<IEnumerable<StatusDto>> Handle(GetStatusesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all statuses");
        
        var statuses = await statusRepository.GetStatusesAsync();
        var statusesDto = mapper.Map<IEnumerable<StatusDto>>(statuses);

        return statusesDto;
    }
}