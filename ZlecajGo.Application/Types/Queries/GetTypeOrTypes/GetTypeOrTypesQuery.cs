using MediatR;
using OneOf;
using ZlecajGo.Application.Types.Dtos;

namespace ZlecajGo.Application.Types.Queries.GetTypeOrTypes;

public record GetTypeOrTypesQuery(int? TypeId) : IRequest<OneOf<TypeDto, IEnumerable<TypeDto>>>;