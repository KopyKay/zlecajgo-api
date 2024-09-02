using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Users.Dtos;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Application.Users.Queries.GetUsers;

public class GetUsersQueryHandler
(
    ILogger<GetUsersQueryHandler> logger,
    UserManager<User> userManager,
    IMapper mapper
)    
: IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all users");
        
        var users = await userManager.Users
            .AsNoTracking()
            .ToListAsync(cancellationToken);
            
        var usersDto = mapper.Map<IEnumerable<UserDto>>(users);

        return usersDto;
    }
}