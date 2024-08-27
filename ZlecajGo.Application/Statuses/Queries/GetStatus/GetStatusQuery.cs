using MediatR;
using ZlecajGo.Application.Statuses.Dtos;

namespace ZlecajGo.Application.Statuses.Queries.GetStatus;

public record GetStatusQuery(int StatusId) : IRequest<StatusDto>;