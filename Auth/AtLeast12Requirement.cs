using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Auth
{
    public class AtLeast12Requirement : IAuthorizationRequirement
    {
        public int RequiredMinimumAge { get; }

        public AtLeast12Requirement(int requiredMinimumAge)
        {
            RequiredMinimumAge = requiredMinimumAge;
        }
    }
}
