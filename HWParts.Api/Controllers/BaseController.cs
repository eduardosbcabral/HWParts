using HWParts.Core.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HWParts.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly DomainNotificationHandler _notifications;

        public BaseController(INotificationHandler<DomainNotification> notifications)
        {
            _notifications = (DomainNotificationHandler)notifications;
        }

        public bool IsValidOperation()
        {
            return (!_notifications.HasNotifications());
        }

        public bool HasNotification(string key) 
        {
            return _notifications.HasNotification(key);
        }

        public T GetNotification<T>()
        {
            var notification = _notifications.GetNotificationByType<T>();
            return (T)Convert.ChangeType(notification, typeof(T));
            if (notification == null) return default;
        }
    }
}
