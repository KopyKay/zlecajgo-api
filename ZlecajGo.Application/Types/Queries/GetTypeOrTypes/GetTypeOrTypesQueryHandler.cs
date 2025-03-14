using AutoMapper;
using MediatR;
using OneOf;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Types.Dtos;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Types.Queries.GetTypeOrTypes;

public class GetTypeOrTypesQueryHandler
(
    ILogger<GetTypeOrTypesQueryHandler> logger,
    ITypeRepository typeRepository,
    IMapper mapper
)    
: IRequestHandler<GetTypeOrTypesQuery, OneOf<TypeDto, IEnumerable<TypeDto>>>
{
    public async Task<OneOf<TypeDto, IEnumerable<TypeDto>>> Handle(GetTypeOrTypesQuery request, CancellationToken cancellationToken)
    {
        if (request.TypeId.HasValue)
        {
            var typeId = request.TypeId.Value;
            
            logger.LogInformation("Getting type with id [{TypeId}]", typeId);

            var type = await typeRepository.GetTypeByIdAsync(typeId)
                       ?? throw new NotFoundException(nameof(Type), typeId.ToString());
        
            var typeDto = mapper.Map<TypeDto>(type);

            return OneOf<TypeDto, IEnumerable<TypeDto>>.FromT0(typeDto);
        }
        
        logger.LogInformation("Getting all types");
        
        var types = await typeRepository.GetTypesAsync();
        var typesDto = mapper.Map<IEnumerable<TypeDto>>(types);

        return OneOf<TypeDto, IEnumerable<TypeDto>>.FromT1(typesDto);
    }
}