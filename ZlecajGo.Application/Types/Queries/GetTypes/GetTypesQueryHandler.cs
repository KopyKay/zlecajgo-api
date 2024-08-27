using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Types.Dtos;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Types.Queries.GetTypes;

public class GetTypesQueryHandler
(
    ILogger<GetTypesQueryHandler> logger,
    ITypeRepository typeRepository,
    IMapper mapper
)    
: IRequestHandler<GetTypesQuery, IEnumerable<TypeDto>>
{
    public async Task<IEnumerable<TypeDto>> Handle(GetTypesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all types");
        
        var types = await typeRepository.GetTypesAsync();
        var typesDto = mapper.Map<IEnumerable<TypeDto>>(types);

        return typesDto;
    }
}