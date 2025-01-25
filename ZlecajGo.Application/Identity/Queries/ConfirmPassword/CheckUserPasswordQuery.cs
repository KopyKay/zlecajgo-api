using MediatR;

namespace ZlecajGo.Application.Identity.Queries.ConfirmPassword;

public record CheckUserPasswordQuery(string Password) : IRequest<bool>;