using MediatR;
using ZlecajGo.Application.Users.Dtos;

namespace ZlecajGo.Application.Users.Queries.GetCurrentUser;

public class GetCurrentUserQuery : IRequest<CurrentUser>;