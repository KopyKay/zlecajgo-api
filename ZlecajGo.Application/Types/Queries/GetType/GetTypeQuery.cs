using MediatR;
using ZlecajGo.Application.Types.Dtos;

namespace ZlecajGo.Application.Types.Queries.GetType;

public record GetTypeQuery(int TypeId) : IRequest<TypeDto>;