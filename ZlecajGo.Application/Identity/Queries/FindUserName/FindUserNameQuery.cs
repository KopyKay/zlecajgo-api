using MediatR;

namespace ZlecajGo.Application.Identity.Queries.FindUserName;

public record class FindUserNameQuery(string Email) : IRequest<string>;