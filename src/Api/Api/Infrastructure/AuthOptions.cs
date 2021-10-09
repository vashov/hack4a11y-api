using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Api.Infrastructure
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена

        private static string KEY { get; } = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");

        public const int LIFETIME = 1;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
