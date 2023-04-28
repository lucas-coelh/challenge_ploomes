using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebAPIs.Token
{
    public class TokenJWTBuilder
    {
        private SecurityKey SecurityKey = null;
        private string Subject = "";
        private string Issuer = "";
        private string Audience = "";
        private Dictionary<string, string> Claims = new Dictionary<string, string>();
        private int ExpiryInMinutes = 8;

        public TokenJWTBuilder AddSecurityKey(SecurityKey securityKey)
        {
            SecurityKey = securityKey;
            return this;
        }

        public TokenJWTBuilder AddSubject(string subject)
        {
            Subject = subject;
            return this;
        }

        public TokenJWTBuilder AddIssuer(string issuer)
        {
            Issuer = issuer;
            return this;
        }

        public TokenJWTBuilder AddAudience(string audience)
        {
            Audience = audience;
            return this;
        }

        public TokenJWTBuilder AddClaim(string type, string value)
        {
            Claims.Add(type, value);
            return this;
        }

        public TokenJWTBuilder AddExpiry(int expiryInMinutes)
        {
            ExpiryInMinutes = expiryInMinutes;
            return this;
        }

        private void EnsureArguments()
        {
            if (this.SecurityKey == null)
                throw new ArgumentNullException("Security Key");

            if (string.IsNullOrEmpty(Subject))
                throw new ArgumentNullException("Subject");

            if (string.IsNullOrEmpty(Issuer))
                throw new ArgumentNullException("Issuer");

            if (string.IsNullOrEmpty(Audience))
                throw new ArgumentNullException("Audience");
        }

        public TokenJWT Builder()
        {
            EnsureArguments();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }.Union(Claims.Select(item => new Claim(item.Key, item.Value)));

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(ExpiryInMinutes),
                signingCredentials: new SigningCredentials(
                                                   SecurityKey,
                                                   SecurityAlgorithms.HmacSha256)

                );

            return new TokenJWT(token);
        }
    }
}