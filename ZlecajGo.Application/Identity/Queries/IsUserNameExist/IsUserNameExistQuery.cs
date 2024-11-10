using MediatR;

namespace ZlecajGo.Application.Identity.Queries.IsUserNameExist;

public record IsUserNameExistQuery(string UserName) : IRequest<bool>;