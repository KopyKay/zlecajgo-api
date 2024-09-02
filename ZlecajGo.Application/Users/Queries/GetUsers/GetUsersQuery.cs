using MediatR;
using ZlecajGo.Application.Users.Dtos;

namespace ZlecajGo.Application.Users.Queries.GetUsers;

public class GetUsersQuery : IRequest<IEnumerable<UserDto>>;