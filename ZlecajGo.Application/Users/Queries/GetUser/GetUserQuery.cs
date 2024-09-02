using MediatR;
using ZlecajGo.Application.Users.Dtos;

namespace ZlecajGo.Application.Users.Queries.GetUser;

public record GetUserQuery(string UserId) : IRequest<UserDto?>;