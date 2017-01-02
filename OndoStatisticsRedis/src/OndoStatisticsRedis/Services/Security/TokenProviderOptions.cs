using Microsoft.IdentityModel.Tokens;
using System;

namespace OndoStatisticsRedis.Services.Security
{
    public class TokenProviderOptions
    {
        public string Path { get; set; } = "/token";

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(30); // SET EXPERATION TIME HERE

        public SigningCredentials SigningCredentials { get; set; }
    }
}
