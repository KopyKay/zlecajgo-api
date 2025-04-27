using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ZlecajGo.Application.OfferContractors.Dtos;
using ZlecajGo.Domain.Constants;

namespace ZlecajGo.Infrastructure.Hubs.Notification;

[Authorize(Policy = PolicyNames.HasProfileCompleted)]
public class NotificationHub : Hub<INotificationHub>
{
    public async Task SendOfferContractNotification(OfferContractorDto offerContractorDto, string offerProviderId, string message)
    {
        await Clients.User(offerProviderId).ReceiveOfferContractNotification(offerContractorDto, message);
    }
}