using MediatR;

namespace ZlecajGo.Application.Users.Queries.ConfirmPassword;

public record CheckUserPasswordQuery(string Password) : IRequest<bool>;