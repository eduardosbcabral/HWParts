using FluentValidation.Results;
using System.Collections.Generic;

namespace HWParts.Core.Domain.Core.Notifications
{
    public interface INotificationContext
	{
		IReadOnlyCollection<Notification> Notifications { get; }
		bool HasNotifications { get; }

		void AddNotification(string key, string message);

		void AddNotification(Notification notification);

		void AddNotifications(IReadOnlyCollection<Notification> notifications);

		void AddNotifications(IList<Notification> notifications);

		void AddNotifications(ICollection<Notification> notifications);
		void AddNotifications(ValidationResult validationResult);

	}
}
