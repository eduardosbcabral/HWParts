using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;

namespace HWParts.Core.Domain.Core.Commands
{
    public class ErrorCommandResponse : CommandResponse, ICommandResponse
    {
        public override object Result
        {
            get
            {
                dynamic resultObj = new ExpandoObject();
                resultObj.status = "error";

                if (!string.IsNullOrEmpty(Message))
                {
                    resultObj.message = Message;
                }

                if (Notifications.Any())
                {
                    var notifications = new List<Notification>(Notifications);

                    if (!string.IsNullOrEmpty(Message))
                    {
                        notifications.RemoveAt(0);
                    }

                    resultObj.errors = notifications
                        .Where(x => !string.IsNullOrEmpty(x.Message) || !string.IsNullOrEmpty(x.Property))
                        .Select(x =>
                        {
                            dynamic obj = new ExpandoObject();

                            if (x.Property != null)
                                obj.description = x.Message;

                            if (x.Property != null)
                                obj.property = x.Property;

                            return obj;
                        });
                }

                return resultObj;
            }
        }

        public ErrorCommandResponse()
        {

        }

        public ErrorCommandResponse(string message)
            : base(message)
        {
            AddNotification(string.Empty, message);
        }
    }
}
