using HWParts.Core.Domain.Core.Commands;
using HWParts.Core.Domain.Core.Notifications;
using HWParts.Core.Domain.Interfaces;

namespace HWParts.Core.Domain.CommandHandlers
{
    public class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly INotificationContext _notificationContext;

        public CommandHandler(IUnitOfWork uow, INotificationContext notificationContext)
        {
            _uow = uow;
            _notificationContext = notificationContext;
        }

        protected void Notify(Notification notification)
        {
            _notificationContext.AddNotification(notification);
        }

        protected void NotifyValidationErrors(Command message)
        {
            _notificationContext.AddNotifications(message.ValidationResult);
        }

        public bool Commit()
        {
            if (_notificationContext.HasNotifications) return false;
            if (_uow.Commit()) return true;

            _notificationContext.AddNotification(new Notification("Commit", "Ocorreu um problema ao salvar os dados."));
            return false;
        }
    }
}
