using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Wetcardboard_Shared.Security.Jwt
{
    public class JwtFunctions : IJwtFunctions
    {
        #region Fields & Properties
        private byte[] KeyBytes
        {
            get
            {
                return Encoding.UTF8.GetBytes(Key);
            }
        }

        private string Audience { get; init; }
        private string Issuer { get; init; }
        private string Key { get; init; }
        #endregion \ Fields & Properties


        #region Constructor
        public JwtFunctions(string issuer, string audience, string key)
        {
            Issuer = issuer;
            Audience = audience;
            Key = key;
        }
        #endregion \ Constructor


        #region Interface Implementations
        #region IJwtFunctions Implementation
        public string GenerateJwtToken(int userId, string userMail, IEnumerable<Claim> userClaims, DateTime expires)
        {
            var claims = new List<Claim>() {
                new Claim("Id", $"{Guid.NewGuid()}"),
                new Claim(JwtRegisteredClaimNames.Sub, $"{userId}"),
                new Claim(JwtRegisteredClaimNames.Email, userMail),
                new Claim(JwtRegisteredClaimNames.Jti, $"{Guid.NewGuid()}"),
            };
            claims.AddRange(userClaims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                Issuer = Issuer,
                Audience = Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(KeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }
        #endregion \ IJwtFunctions Implementation
        #endregion \ Interface Implementations
    }
}
