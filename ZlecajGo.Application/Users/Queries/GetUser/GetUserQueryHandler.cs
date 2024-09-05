using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Users.Dtos;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;

namespace ZlecajGo.Application.Users.Queries.GetUser;

public class GetUserQueryHandler
(
    ILogger<GetUserQueryHandler> logger,
    UserManager<User> userManager,
    IMapper mapper
)    
: IRequestHandler<GetUserQuery, UserDto?>
{
    public async Task<UserDto?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting user with id [{UserId}]", request.UserId);
        
        var user = await userManager.FindByIdAsync(request.UserId)
            ?? throw new NotFoundException(nameof(User), request.UserId);
        
        var userDto = mapper.Map<UserDto>(user);
        
        return userDto;
    }
}