using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Users.Dtos;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Application.Users.Queries.GetCurrentUser;

public class GetCurrentUserQueryHandler 
(
    ILogger<GetCurrentUserQueryHandler> logger,
    IUserStore<User> userStore,
    IUserContext userContext,
    IMapper mapper
)
: IRequestHandler<GetCurrentUserQuery, UserDto>
{
    public async Task<UserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser()!;
        
        logger.LogInformation("Getting current user with id [{UserId}]", currentUser.Id);

        var user = await userStore.FindByIdAsync(currentUser.Id, cancellationToken);
        
        var userDto = mapper.Map<UserDto>(user);

        return userDto;
    }
}