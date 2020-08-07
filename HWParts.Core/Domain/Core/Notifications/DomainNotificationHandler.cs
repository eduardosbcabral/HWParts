using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Core.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public Task Handle(DomainNotification message, CancellationToken cancellationToken)
        {
            _notifications.Add(message);

            return Task.CompletedTask;
        }

        public virtual List<DomainNotification> GetNotifications()
        {
            return _notifications;
        }

        public virtual bool HasNotifications()
        {
            return GetNotifications().Any();
        }

        public virtual bool HasNotification(string key)
        {
            return GetNotifications().Any(x => x.Key == key);
        }

        public virtual DomainNotification GetNotification(string key)
        {
            return GetNotifications().SingleOrDefault(x => x.Key == key);
        }

        public virtual DomainNotification GetNotificationByType<T>()
        {
            return GetNotifications().SingleOrDefault(x => x.GetType() == typeof(T));
        }

        public void Dispose()
        {
            _notifications = new List<DomainNotification>();
        }
    }
}
