using MediatR;
using ZlecajGo.Application.Statuses.Dtos;

namespace ZlecajGo.Application.Statuses.Queries.GetStatuses;

public class GetStatusesQuery : IRequest<IEnumerable<StatusDto>>;