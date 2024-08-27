using MediatR;
using ZlecajGo.Application.Types.Dtos;

namespace ZlecajGo.Application.Types.Queries.GetTypes;

public class GetTypesQuery : IRequest<IEnumerable<TypeDto>>;