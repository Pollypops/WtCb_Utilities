using System.Security.Claims;

namespace Wetcardboard_Shared.Security.Jwt
{
    public interface IJwtFunctions
    {
        string GenerateJwtToken(int userId, string userMail, IEnumerable<Claim> userClaims, DateTime expires);
    }
}
