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
    }
}
