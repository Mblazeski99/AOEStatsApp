using AOEStatsApp.ViewModels.DomainViewModels;
using Domain.Enums;
using Domain.Models;

namespace AOEStatsApp.ViewModels
{
    public class NotificationViewModel : DomainViewModelBase
    {
        private readonly Notification _notification;

        public string Message => _notification.Message;
        public MessageType MessageType => _notification.MessageType;

        public NotificationViewModel(Notification notification) : base(notification)
        {
            _notification = notification;
        }
    }
}
