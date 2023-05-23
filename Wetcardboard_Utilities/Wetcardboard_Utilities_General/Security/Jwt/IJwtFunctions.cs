using System.Security.Claims;

namespace Wetcardboard_Utilities_General.Security.Jwt
{
    public interface IJwtFunctions
    {
        string GenerateJwtToken(int userId, string userMail, IEnumerable<Claim> userClaims, DateTime expires);
    }
}
