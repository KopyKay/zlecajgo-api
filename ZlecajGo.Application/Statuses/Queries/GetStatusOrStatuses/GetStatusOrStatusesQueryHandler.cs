using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using ZlecajGo.Application.Statuses.Dtos;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Statuses.Queries.GetStatusOrStatuses;

public class GetStatusOrStatusesQueryHandler
(
    ILogger<GetStatusOrStatusesQueryHandler> logger,
    IStatusRepository statusRepository,
    IMapper mapper
)
: IRequestHandler<GetStatusOrStatusesQuery, OneOf<StatusDto, IEnumerable<StatusDto>>>
{
    public async Task<OneOf<StatusDto, IEnumerable<StatusDto>>> Handle(GetStatusOrStatusesQuery request, CancellationToken cancellationToken)
    {
        if (request.StatusId.HasValue)
        {
            var statusId = request.StatusId.Value;
            
            logger.LogInformation("Getting status with id [{StatusId}]", statusId);
        
            var status = await statusRepository.GetStatusByIdAsync(statusId)
                         ?? throw new NotFoundException(nameof(Status), statusId.ToString());
        
            var statusDto = mapper.Map<StatusDto>(status);

            return OneOf<StatusDto, IEnumerable<StatusDto>>.FromT0(statusDto);
        }
        
        logger.LogInformation("Getting all statuses");
        
        var statuses = await statusRepository.GetStatusesAsync();
        var statusesDto = mapper.Map<IEnumerable<StatusDto>>(statuses);

        return OneOf<StatusDto, IEnumerable<StatusDto>>.FromT1(statusesDto);
    }
}