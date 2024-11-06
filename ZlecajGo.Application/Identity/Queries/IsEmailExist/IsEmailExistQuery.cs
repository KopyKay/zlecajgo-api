using MediatR;

namespace ZlecajGo.Application.Identity.Queries.IsEmailExist;

public record IsEmailExistQuery(string Email) : IRequest<bool>;