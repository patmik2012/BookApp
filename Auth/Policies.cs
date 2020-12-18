using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Auth
{
    public class Policies
    { 
        public const string Admin = "Admin";
        public const string User = "User";
        public const string UserAndAdmin = "UserAndAdmin";
        public const string Adult = "Adult";
        public const int AgeLimit = 12;

        public static AuthorizationPolicy UserPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(User).Build();
        }

        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();
        }

        public static AuthorizationPolicy UserAndAdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).RequireRole(User).Build();
        }

        public static AuthorizationPolicy AtLeast12Policy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(User).RequireClaim("AtLeast12").Build();
        }
    }
}

