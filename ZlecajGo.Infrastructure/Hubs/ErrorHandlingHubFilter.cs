using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Domain.Exceptions;

namespace ZlecajGo.Infrastructure.Hubs;

internal class ErrorHandlingHubFilter(ILogger<ErrorHandlingHubFilter> logger) : IHubFilter
{
    public async ValueTask<object?> InvokeMethodAsync(HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object?>> next)
    {
        try
        {
            return await next(invocationContext);
        }
        catch (NullHubContextException ex)
        {
            logger.LogWarning(ex.Message);
            await SendErrorToClient(invocationContext.Hub, ex.Message, "Forbidden");
            return null;
        }
        catch (NotFoundException ex)
        {
            logger.LogWarning(ex.Message);
            await SendErrorToClient(invocationContext.Hub, ex.Message, "NotFound");
            return null;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            await SendErrorToClient(invocationContext.Hub, "Something went wrong!", "InternalError");
            return null;
        }
    }
    
    private static Task SendErrorToClient(Hub hub, string message, string errorType)
    {
        return hub.Clients.Caller.SendAsync("ReceiveError", new { Message = message, Type = errorType });
    }
}