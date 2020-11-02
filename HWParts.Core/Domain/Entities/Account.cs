using Microsoft.AspNetCore.Identity;

namespace HWParts.Core.Domain.Entities
{
    public class Account : IdentityUser
    {
        public Account()
        {

        }

        public Account(string username)
        {
            UserName = username;
        }

        public Account(string username, string email)
        {
            UserName = username;
            Email = email;
        }

        public void HideIdentityFields()
        {

        }
    }
}
