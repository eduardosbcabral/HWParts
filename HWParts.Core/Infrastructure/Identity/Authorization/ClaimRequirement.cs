﻿using Microsoft.AspNetCore.Authorization;

namespace HWParts.Core.Infrastructure.Identity.Authorization
{
    public class ClaimRequirement : IAuthorizationRequirement
    {
        public string ClaimName { get; set; }
        public string ClaimValue { get; set; }

        public ClaimRequirement(string claimName, string claimValue)
        {
            ClaimName = claimName;
            ClaimValue = claimValue;
        }
    }
}
