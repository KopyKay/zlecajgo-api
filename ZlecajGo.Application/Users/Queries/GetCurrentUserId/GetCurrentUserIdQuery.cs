using MediatR;

namespace ZlecajGo.Application.Users.Queries.GetCurrentUserId;

public class GetCurrentUserIdQuery : IRequest<string>;