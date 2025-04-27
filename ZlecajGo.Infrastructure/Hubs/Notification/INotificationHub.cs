using ZlecajGo.Application.OfferContractors.Dtos;

namespace ZlecajGo.Infrastructure.Hubs.Notification;

public interface INotificationHub
{
    Task ReceiveOfferContractNotification(OfferContractorDto offerContractorDto, string message);
}