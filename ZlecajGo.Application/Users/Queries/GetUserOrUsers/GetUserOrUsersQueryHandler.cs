using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneOf;
using ZlecajGo.Application.Users.Dtos;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;

namespace ZlecajGo.Application.Users.Queries.GetUserOrUsers;

public class GetUserOrUsersQueryHandler
(
    ILogger<GetUserOrUsersQueryHandler> logger,
    UserManager<User> userManager,
    IMapper mapper
)    
: IRequestHandler<GetUserOrUsersQuery, OneOf<UserDto, IEnumerable<UserDto>>>
{
    public async Task<OneOf<UserDto, IEnumerable<UserDto>>> Handle(GetUserOrUsersQuery request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.UserId))
        {
            logger.LogInformation("Getting user with id [{UserId}]", request.UserId);
        
            var user = await userManager.FindByIdAsync(request.UserId)
                       ?? throw new NotFoundException(nameof(User), request.UserId);
        
            var userDto = mapper.Map<UserDto>(user);
        
            return OneOf<UserDto, IEnumerable<UserDto>>.FromT0(userDto);
        }
        
        logger.LogInformation("Getting all users");
        
        var users = await userManager.Users
            .AsNoTracking()
            .ToListAsync(cancellationToken);
            
        var usersDto = mapper.Map<IEnumerable<UserDto>>(users);

        return OneOf<UserDto, IEnumerable<UserDto>>.FromT1(usersDto);
    }
}