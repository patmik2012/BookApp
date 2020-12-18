using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Auth
{
    public class Token
    {
        public const string Issuer = "https://localhost/";
        public const string Audience = Issuer;
        public const string SecretKey = "SecretKey_123456";
    }
}
