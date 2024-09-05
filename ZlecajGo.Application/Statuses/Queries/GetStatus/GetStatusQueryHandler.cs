using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Statuses.Dtos;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Statuses.Queries.GetStatus;

public class GetStatusQueryHandler
(
    ILogger<GetStatusQueryHandler> logger,
    IStatusRepository statusRepository,
    IMapper mapper
)    
: IRequestHandler<GetStatusQuery, StatusDto>
{
    public async Task<StatusDto> Handle(GetStatusQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting status with id [{StatusId}]", request.StatusId);
        
        var status = await statusRepository.GetStatusByIdAsync(request.StatusId)
            ?? throw new NotFoundException(nameof(Status), request.StatusId.ToString());
        
        var statusDto = mapper.Map<StatusDto>(status);

        return statusDto;
    }
}