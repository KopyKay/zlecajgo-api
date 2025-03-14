using MediatR;
using OneOf;
using ZlecajGo.Application.Statuses.Dtos;

namespace ZlecajGo.Application.Statuses.Queries.GetStatusOrStatuses;

public record GetStatusOrStatusesQuery(int? StatusId) : IRequest<OneOf<StatusDto, IEnumerable<StatusDto>>>;