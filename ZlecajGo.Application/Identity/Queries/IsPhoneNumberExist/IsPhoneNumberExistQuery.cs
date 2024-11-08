using MediatR;

namespace ZlecajGo.Application.Identity.Queries.IsPhoneNumberExist;

public record IsPhoneNumberExistQuery(string PhoneNumber) : IRequest<bool>;