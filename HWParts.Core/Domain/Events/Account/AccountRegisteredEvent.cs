using HWParts.Core.Domain.Core.Events;
using System;

namespace HWParts.Core.Domain.Events
{
    public class AccountRegisteredEvent : Event
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public AccountRegisteredEvent(string id, string username, string email, string token)
        {
            Id = id;
            Username = username;
            Email = email;
            Token = token;
        }
    }
}
