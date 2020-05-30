using HWParts.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace HWParts.Core.Infrastructure.Identity.Models
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _acessor;

        public AspNetUser(IHttpContextAccessor acessor)
        {
            _acessor = acessor;
        }

        public string Name => GetName();

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _acessor.HttpContext.User.Claims;
        }

        public bool IsAuthenticated()
        {
            return _acessor.HttpContext.User.Identity.IsAuthenticated;
        }

        private string GetName()
        {
            return _acessor.HttpContext.User.Identity.Name ??
                _acessor.HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        }
    }
}
