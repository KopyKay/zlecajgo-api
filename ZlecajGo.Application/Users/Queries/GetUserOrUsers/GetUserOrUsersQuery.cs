using MediatR;
using OneOf;
using ZlecajGo.Application.Users.Dtos;

namespace ZlecajGo.Application.Users.Queries.GetUserOrUsers;

public record GetUserOrUsersQuery(string? UserId) : IRequest<OneOf<UserDto, IEnumerable<UserDto>>>;