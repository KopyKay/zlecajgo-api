using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Types.Dtos;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;
using Type = ZlecajGo.Domain.Entities.Type;

namespace ZlecajGo.Application.Types.Queries.GetType;

public class GetTypeQueryHandler 
(
    ILogger<GetTypeQueryHandler> logger,
    ITypeRepository typeRepository,
    IMapper mapper
)    
: IRequestHandler<GetTypeQuery, TypeDto>
{
    public async Task<TypeDto> Handle(GetTypeQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting type with id: {TypeId}", request.TypeId);

        var type = await typeRepository.GetTypeByIdAsync(request.TypeId)
            ?? throw new NotFoundException(nameof(Type), request.TypeId.ToString());
        
        var typeDto = mapper.Map<TypeDto>(type);

        return typeDto;
    }
}