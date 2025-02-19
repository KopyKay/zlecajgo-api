using MediatR;

namespace ZlecajGo.Application.Users.Commands.ConfirmPassword;

public record CheckUserPasswordCommand(string Password) : IRequest<bool>;